using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ImageClicker.Properties;
using OpenCvSharp;

namespace ImageClicker;

public partial class Form1 : Form
{
    private CancellationTokenSource? _cts;
    private Rectangle? _detectionRegion;
    private readonly Random _random = new();
    private DateTime _nextBreak;

    private readonly Dictionary<string, DateTime> _imageCooldowns = new(
        StringComparer.OrdinalIgnoreCase
    );

    private const int ImageCooldownMs = 1000;

    [DllImport("user32.dll")]
    private static extern void mouse_event(
        uint dwFlags,
        uint dx,
        uint dy,
        uint dwData,
        nuint dwExtraInfo
    );

    [DllImport("user32.dll")]
    private static extern IntPtr WindowFromPoint(System.Drawing.Point point);

    [DllImport("user32.dll")]
    private static extern bool ScreenToClient(IntPtr hWnd, ref System.Drawing.Point point);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;

    private const uint WM_LBUTTONDOWN = 0x0201;
    private const uint WM_LBUTTONUP = 0x0202;

    private const int MK_LBUTTON = 0x0001;

    public Form1()
    {
        InitializeComponent();

        string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.ico");

        if (File.Exists(iconPath))
        {
            try
            {
                Icon = new Icon(iconPath);
            }
            catch { }
        }

        LoadSettingsIntoUI();

        LogInfo("Application started.");
        LogInfo("Ready.");
    }

    private void LoadSettingsIntoUI()
    {
        txtFolder.Text = Settings.Default.ImageFolder;

        txtPrecision.Text = Settings.Default.Threshold.ToString(
            System.Globalization.CultureInfo.InvariantCulture
        );

        txtScanMin.Text = Settings.Default.ScanMin.ToString();

        txtScanMax.Text = Settings.Default.ScanMax.ToString();

        txtBeforeMin.Text = Settings.Default.BeforeClickMin.ToString();

        txtBeforeMax.Text = Settings.Default.BeforeClickMax.ToString();

        txtAfterMin.Text = Settings.Default.AfterClickMin.ToString();

        txtAfterMax.Text = Settings.Default.AfterClickMax.ToString();

        txtClickMin.Text = Settings.Default.ClickMarginMin.ToString();

        txtClickMax.Text = Settings.Default.ClickMarginMax.ToString();

        txtBreakEveryMin.Text = Settings.Default.BreakEveryMin.ToString();

        txtBreakEveryMax.Text = Settings.Default.BreakEveryMax.ToString();

        txtBreakDurationMin.Text = Settings.Default.BreakDurationMin.ToString();

        txtBreakDurationMax.Text = Settings.Default.BreakDurationMax.ToString();

        chkBackgroundClick.Checked = Settings.Default.BackgroundClick;

        chkDebug.Checked = false;
    }

    private bool DebugMode => chkDebug != null && chkDebug.Checked;

    private void SaveSettings()
    {
        Settings.Default.ImageFolder = txtFolder.Text.Trim();

        Settings.Default.Threshold = ReadDouble(txtPrecision, 0.88, 0.1, 1.0);

        Settings.Default.ScanMin = ReadInt(txtScanMin, 100, 0, 60000);

        Settings.Default.ScanMax = ReadInt(txtScanMax, 300, 0, 60000);

        Settings.Default.BeforeClickMin = ReadInt(txtBeforeMin, 100, 0, 60000);

        Settings.Default.BeforeClickMax = ReadInt(txtBeforeMax, 200, 0, 60000);

        Settings.Default.AfterClickMin = ReadInt(txtAfterMin, 500, 0, 60000);

        Settings.Default.AfterClickMax = ReadInt(txtAfterMax, 1000, 0, 60000);

        Settings.Default.ClickMarginMin = ReadInt(txtClickMin, 15, 0, 100);

        Settings.Default.ClickMarginMax = ReadInt(txtClickMax, 85, 0, 100);

        Settings.Default.BreakEveryMin = ReadInt(txtBreakEveryMin, 20, 1, 1440);

        Settings.Default.BreakEveryMax = ReadInt(txtBreakEveryMax, 30, 1, 1440);

        Settings.Default.BreakDurationMin = ReadInt(txtBreakDurationMin, 1, 1, 120);

        Settings.Default.BreakDurationMax = ReadInt(txtBreakDurationMax, 2, 1, 120);

        Settings.Default.BackgroundClick = chkBackgroundClick.Checked;

        Settings.Default.Save();
    }

    private static int ReadInt(TextBox box, int fallback, int min, int max)
    {
        if (!int.TryParse(box.Text, out int value))
        {
            return fallback;
        }

        return Math.Clamp(value, min, max);
    }

    private static double ReadDouble(TextBox box, double fallback, double min, double max)
    {
        if (
            !double.TryParse(
                box.Text,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out double value
            )
        )
        {
            return fallback;
        }

        return Math.Clamp(value, min, max);
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        if (_cts != null)
            return;

        SaveSettings();

        string folder = txtFolder.Text.Trim();

        Directory.CreateDirectory(folder);

        string[] images = GetImageFiles(folder);

        if (images.Length == 0)
        {
            LogWarning("No images found in the selected folder.");

            return;
        }

        _imageCooldowns.Clear();

        _cts = new CancellationTokenSource();

        ScheduleNextBreak();

        btnStart.Enabled = false;
        btnStop.Enabled = true;

        LogSuccess($"Detector started. {images.Length} image(s) loaded.");

        LogInfo($"Next break scheduled at {_nextBreak:HH:mm:ss}");

        if (DebugMode)
        {
            LogInfo("Debug mode: ON");
        }

        CancellationToken token = _cts.Token;

        Task.Run(() => DetectionLoop(token));
    }

    private void btnStop_Click(object? sender, EventArgs e)
    {
        StopDetection();
    }

    private void StopDetection()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _imageCooldowns.Clear();

        if (!IsDisposed)
        {
            try
            {
                BeginInvoke(() =>
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                });
            }
            catch { }
        }

        LogInfo("Detector stopped.");
    }

    private void ScheduleNextBreak()
    {
        int min = Math.Max(1, Settings.Default.BreakEveryMin);

        int max = Math.Max(min, Settings.Default.BreakEveryMax);

        int minutes = RandomValue(min, max);

        _nextBreak = DateTime.Now.AddMinutes(minutes);
    }

    private void DetectionLoop(CancellationToken token)
    {
        if (DebugMode)
        {
            LogInfo(">>> LOOP STARTED <<<");
        }

        while (!token.IsCancellationRequested)
        {
            try
            {
                if (DateTime.Now >= _nextBreak)
                {
                    PerformBreak(token);

                    if (token.IsCancellationRequested)
                        break;

                    ScheduleNextBreak();

                    LogInfo($"Next break scheduled at " + $"{_nextBreak:HH:mm:ss}");
                }

                int scanDelay = RandomValue(Settings.Default.ScanMin, Settings.Default.ScanMax);

                if (DebugMode)
                {
                    LogInfo($"Next scan in {scanDelay} ms.");
                }

                if (!WaitWithCancellation(scanDelay, token))
                {
                    break;
                }

                if (token.IsCancellationRequested)
                    break;

                ScanForImages(token);
            }
            catch (Exception ex)
            {
                LogError($"Detection error: {ex.Message}");

                if (!WaitWithCancellation(1000, token))
                {
                    break;
                }
            }
        }
    }

    private void PerformBreak(CancellationToken token)
    {
        int minutes = RandomValue(
            Settings.Default.BreakDurationMin,
            Settings.Default.BreakDurationMax
        );

        TimeSpan duration = TimeSpan.FromMinutes(minutes);

        LogWarning($"Break started. Duration: " + $"{duration.TotalMinutes:0.#} minute(s).");

        DateTime end = DateTime.Now.Add(duration);

        while (DateTime.Now < end && !token.IsCancellationRequested)
        {
            if (!WaitWithCancellation(500, token))
            {
                break;
            }
        }

        if (!token.IsCancellationRequested)
        {
            LogSuccess("Break finished.");
        }
    }

    private void ScanForImages(CancellationToken token)
    {
        string folder = txtFolder.Text.Trim();

        string[] images = GetImageFiles(folder);

        if (images.Length == 0)
        {
            LogWarning("No image files found.");

            return;
        }

        if (DebugMode)
        {
            LogInfo($"Scanning {images.Length} image(s)...");
        }

        using Bitmap screenshot = CaptureScreen();

        using Mat screen = BitmapToMat(screenshot);

        System.Drawing.Rectangle virtualScreen = SystemInformation.VirtualScreen;

        Mat searchImage = screen;

        int offsetX = virtualScreen.X;

        int offsetY = virtualScreen.Y;

        Mat? cropped = null;

        try
        {
            if (_detectionRegion.HasValue)
            {
                System.Drawing.Rectangle region = _detectionRegion.Value;

                System.Drawing.Rectangle relative = new(
                    region.X - virtualScreen.X,
                    region.Y - virtualScreen.Y,
                    region.Width,
                    region.Height
                );

                relative = System.Drawing.Rectangle.Intersect(
                    relative,
                    new System.Drawing.Rectangle(0, 0, screen.Width, screen.Height)
                );

                if (relative.Width <= 0 || relative.Height <= 0)
                {
                    return;
                }

                cropped = new Mat(
                    screen,
                    new OpenCvSharp.Rect(relative.X, relative.Y, relative.Width, relative.Height)
                );

                searchImage = cropped;

                offsetX += relative.X;
                offsetY += relative.Y;
            }

            foreach (string imageFile in images)
            {
                if (token.IsCancellationRequested)
                    return;

                string fileName = Path.GetFileName(imageFile);

                if (IsOnCooldown(fileName))
                {
                    if (DebugMode)
                    {
                        LogInfo($"Cooldown: {fileName}");
                    }

                    continue;
                }

                using Mat template = Cv2.ImRead(imageFile, ImreadModes.Color);

                if (template.Empty())
                    continue;

                if (template.Width > searchImage.Width || template.Height > searchImage.Height)
                {
                    continue;
                }

                using Mat result = new();

                Cv2.MatchTemplate(searchImage, template, result, TemplateMatchModes.CCoeffNormed);

                Cv2.MinMaxLoc(
                    result,
                    out _,
                    out double maxValue,
                    out _,
                    out OpenCvSharp.Point maxLocation
                );

                double threshold = Settings.Default.Threshold;

                if (DebugMode)
                {
                    LogInfo(
                        $"{fileName}: " + $"score={maxValue:F4} " + $"threshold={threshold:F4}"
                    );
                }

                if (maxValue < threshold)
                    continue;

                int imageX = offsetX + maxLocation.X;

                int imageY = offsetY + maxLocation.Y;

                int clickX = GetRandomClickCoordinate(imageX, template.Width);

                int clickY = GetRandomClickCoordinate(imageY, template.Height);

                LogSuccess(
                    $"Detected: {fileName} " + $"score={maxValue:F4} " + $"at {imageX},{imageY}"
                );

                int beforeClick = RandomValue(
                    Settings.Default.BeforeClickMin,
                    Settings.Default.BeforeClickMax
                );

                if (!WaitWithCancellation(beforeClick, token))
                {
                    return;
                }

                if (token.IsCancellationRequested)
                    return;

                bool clicked;

                if (Settings.Default.BackgroundClick)
                {
                    clicked = BackgroundClick(clickX, clickY);

                    if (clicked)
                    {
                        LogSuccess($"Background click sent at " + $"{clickX},{clickY}");
                    }
                    else
                    {
                        LogWarning($"Background click failed at " + $"{clickX},{clickY}");
                    }
                }
                else
                {
                    PhysicalClick(clickX, clickY);

                    clicked = true;

                    LogSuccess($"Physical click at " + $"{clickX},{clickY}");
                }

                if (clicked)
                {
                    _imageCooldowns[fileName] = DateTime.UtcNow.AddMilliseconds(ImageCooldownMs);
                }

                int afterClick = RandomValue(
                    Settings.Default.AfterClickMin,
                    Settings.Default.AfterClickMax
                );

                if (!WaitWithCancellation(afterClick, token))
                {
                    return;
                }

                return;
            }
        }
        finally
        {
            cropped?.Dispose();
        }
    }

    private bool IsOnCooldown(string fileName)
    {
        if (!_imageCooldowns.TryGetValue(fileName, out DateTime cooldownUntil))
        {
            return false;
        }

        if (DateTime.UtcNow >= cooldownUntil)
        {
            _imageCooldowns.Remove(fileName);
            return false;
        }

        return true;
    }

    private static bool WaitWithCancellation(int milliseconds, CancellationToken token)
    {
        if (milliseconds <= 0)
            return !token.IsCancellationRequested;

        try
        {
            Task.Delay(milliseconds, token).GetAwaiter().GetResult();

            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }

    private int GetRandomClickCoordinate(int imageStart, int imageSize)
    {
        int minPercent = Math.Clamp(Settings.Default.ClickMarginMin, 0, 100);

        int maxPercent = Math.Clamp(Settings.Default.ClickMarginMax, 0, 100);

        if (maxPercent < minPercent)
        {
            (minPercent, maxPercent) = (maxPercent, minPercent);
        }

        int min = imageStart + imageSize * minPercent / 100;

        int max = imageStart + imageSize * maxPercent / 100;

        if (max <= min)
        {
            return imageStart + imageSize / 2;
        }

        return RandomValue(min, max);
    }

    private bool BackgroundClick(int screenX, int screenY)
    {
        IntPtr hwnd = WindowFromPoint(new System.Drawing.Point(screenX, screenY));

        if (hwnd == IntPtr.Zero)
            return false;

        System.Drawing.Point point = new(screenX, screenY);

        if (!ScreenToClient(hwnd, ref point))
        {
            return false;
        }

        IntPtr lParam = (IntPtr)((point.Y << 16) | (point.X & 0xFFFF));

        SendMessage(hwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, lParam);

        SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, lParam);

        return true;
    }

    private static void PhysicalClick(int x, int y)
    {
        System.Drawing.Point originalPosition = Cursor.Position;

        try
        {
            Cursor.Position = new System.Drawing.Point(x, y);

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        finally
        {
            Cursor.Position = originalPosition;
        }
    }

    private static int RandomValue(int min, int max)
    {
        if (max < min)
        {
            (min, max) = (max, min);
        }

        if (min == max)
            return min;

        return Random.Shared.Next(min, max + 1);
    }

    private static string[] GetImageFiles(string folder)
    {
        if (!Directory.Exists(folder))
            return Array.Empty<string>();

        return Directory
            .GetFiles(folder)
            .Where(x =>
                x.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                || x.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                || x.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                || x.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)
            )
            .ToArray();
    }

    private static Bitmap CaptureScreen()
    {
        System.Drawing.Rectangle bounds = SystemInformation.VirtualScreen;

        Bitmap bitmap = new(bounds.Width, bounds.Height, PixelFormat.Format24bppRgb);

        using Graphics graphics = Graphics.FromImage(bitmap);

        graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);

        return bitmap;
    }

    private static Mat BitmapToMat(Bitmap bitmap)
    {
        using MemoryStream stream = new();

        bitmap.Save(stream, ImageFormat.Bmp);

        return Cv2.ImDecode(stream.ToArray(), ImreadModes.Color);
    }

    private void btnOpenFolder_Click(object? sender, EventArgs e)
    {
        try
        {
            string folder = txtFolder.Text.Trim();

            Directory.CreateDirectory(folder);

            SaveSettings();

            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"\"{folder}\"",
                    UseShellExecute = true,
                }
            );

            if (DebugMode)
            {
                LogInfo($"Opened image folder: {folder}");
            }
        }
        catch (Exception ex)
        {
            LogError($"Could not open folder: {ex.Message}");
        }
    }

    private void btnSelectRegion_Click(object? sender, EventArgs e)
    {
        Hide();

        Thread.Sleep(250);

        using RegionSelector selector = new();

        if (selector.ShowDialog() == DialogResult.OK)
        {
            _detectionRegion = selector.SelectedRegion;

            System.Drawing.Rectangle r = _detectionRegion.Value;

            LogInfo($"Detection area: " + $"{r.Width}x{r.Height} " + $"at {r.X},{r.Y}");
        }

        Show();
    }

    private void btnClearRegion_Click(object? sender, EventArgs e)
    {
        _detectionRegion = null;

        LogInfo("Detection area cleared. " + "Full virtual screen enabled.");
    }

    private void LogInfo(string message)
    {
        WriteLog(message, Color.LightSkyBlue);
    }

    private void LogSuccess(string message)
    {
        WriteLog(message, Color.LightGreen);
    }

    private void LogWarning(string message)
    {
        WriteLog(message, Color.Orange);
    }

    private void LogError(string message)
    {
        WriteLog(message, Color.IndianRed);
    }

    private void WriteLog(string message, Color color)
    {
        if (IsDisposed)
            return;

        try
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => WriteLog(message, color));

                return;
            }

            string text = $"[{DateTime.Now:HH:mm:ss}] " + $"{message}\r\n";

            txtLog.SelectionStart = txtLog.TextLength;

            txtLog.SelectionLength = 0;

            txtLog.SelectionColor = color;

            txtLog.AppendText(text);

            txtLog.SelectionColor = Color.White;

            txtLog.SelectionStart = txtLog.TextLength;

            txtLog.ScrollToCaret();
        }
        catch { }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        SaveSettings();

        _cts?.Cancel();

        base.OnFormClosing(e);
    }
}
