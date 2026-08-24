namespace ImageClicker;

public sealed class RegionSelector : Form
{
    private Point startPoint;
    private Point currentPoint;
    private bool selecting;

    public Rectangle SelectedRegion { get; private set; }

    public RegionSelector()
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;

        Bounds = SystemInformation.VirtualScreen;

        TopMost = true;
        ShowInTaskbar = false;

        BackColor = Color.Black;
        Opacity = 0.25;

        Cursor = Cursors.Cross;

        DoubleBuffered = true;
        KeyPreview = true;

        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseUp += OnMouseUp;
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    private void OnMouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;

        selecting = true;
        startPoint = e.Location;
        currentPoint = e.Location;

        Invalidate();
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (!selecting)
            return;

        currentPoint = e.Location;

        Invalidate();
    }

    private void OnMouseUp(object? sender, MouseEventArgs e)
    {
        if (!selecting || e.Button != MouseButtons.Left)
            return;

        selecting = false;
        currentPoint = e.Location;

        Rectangle local = CreateRectangle(startPoint, currentPoint);

        if (local.Width < 10 || local.Height < 10)
            return;

        SelectedRegion = new Rectangle(
            Bounds.X + local.X,
            Bounds.Y + local.Y,
            local.Width,
            local.Height
        );

        DialogResult = DialogResult.OK;
        Close();
    }

    private static Rectangle CreateRectangle(Point a, Point b)
    {
        return new Rectangle(
            Math.Min(a.X, b.X),
            Math.Min(a.Y, b.Y),
            Math.Abs(a.X - b.X),
            Math.Abs(a.Y - b.Y)
        );
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (!selecting)
            return;

        Rectangle rectangle = CreateRectangle(startPoint, currentPoint);

        using Pen pen = new(Color.Red, 3);

        e.Graphics.DrawRectangle(pen, rectangle);

        using Font font = new("Segoe UI", 11);

        e.Graphics.DrawString(
            $"{rectangle.Width} x {rectangle.Height}",
            font,
            Brushes.White,
            rectangle.X,
            Math.Max(0, rectangle.Y - 25)
        );
    }
}
