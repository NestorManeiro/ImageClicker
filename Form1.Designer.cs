namespace ImageClicker;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    private Label lblFolder = null!;
    private TextBox txtFolder = null!;
    private Button btnOpenFolder = null!;

    private Label lblPrecision = null!;
    private TextBox txtPrecision = null!;

    private Label lblScan = null!;
    private TextBox txtScanMin = null!;
    private TextBox txtScanMax = null!;

    private Label lblBeforeClick = null!;
    private TextBox txtBeforeMin = null!;
    private TextBox txtBeforeMax = null!;

    private Label lblAfterClick = null!;
    private TextBox txtAfterMin = null!;
    private TextBox txtAfterMax = null!;

    private Label lblClickArea = null!;
    private TextBox txtClickMin = null!;
    private TextBox txtClickMax = null!;

    private Label lblBreakEvery = null!;
    private TextBox txtBreakEveryMin = null!;
    private TextBox txtBreakEveryMax = null!;

    private Label lblBreakDuration = null!;
    private TextBox txtBreakDurationMin = null!;
    private TextBox txtBreakDurationMax = null!;

    private CheckBox chkBackgroundClick = null!;
    private CheckBox chkDebug = null!;

    private Button btnSelectRegion = null!;
    private Button btnClearRegion = null!;
    private Button btnStart = null!;
    private Button btnStop = null!;

    private RichTextBox txtLog = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        // =========================================================
        // FORM
        // =========================================================

        BackColor = Color.FromArgb(22, 22, 24);
        ForeColor = Color.White;
        ClientSize = new Size(760, 760);

        Text = "Image Clicker";
        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = true;

        Font = new Font(
            "Segoe UI",
            9F,
            FontStyle.Regular);

        // =========================================================
        // TITLE
        // =========================================================

        Label lblTitle = new();

        lblTitle.Text = "IMAGE CLICKER";
        lblTitle.Location = new Point(20, 15);
        lblTitle.AutoSize = true;

        lblTitle.Font = new Font(
            "Segoe UI Semibold",
            16F,
            FontStyle.Bold);

        lblTitle.ForeColor =
            Color.FromArgb(235, 235, 235);

        Controls.Add(lblTitle);

        Label lblSubtitle = new();

        lblSubtitle.Text =
            "Image detection and automated clicking";

        lblSubtitle.Location =
            new Point(22, 43);

        lblSubtitle.AutoSize = true;

        lblSubtitle.Font =
            new Font(
                "Segoe UI",
                8.5F);

        lblSubtitle.ForeColor =
            Color.FromArgb(145, 145, 150);

        Controls.Add(lblSubtitle);

        // =========================================================
        // LEFT PANEL
        // =========================================================

        Panel leftPanel = new();

        leftPanel.Location =
            new Point(15, 75);

        leftPanel.Size =
            new Size(350, 405);

        leftPanel.BackColor =
            Color.FromArgb(30, 30, 33);

        leftPanel.BorderStyle =
            BorderStyle.FixedSingle;

        Controls.Add(leftPanel);

        // =========================================================
        // IMAGES SECTION
        // =========================================================

        Label lblImagesSection = CreateSectionTitle(
            "IMAGE DETECTION",
            new Point(15, 15));

        leftPanel.Controls.Add(lblImagesSection);

        lblFolder = new();

        lblFolder.Text = "Images folder";
        lblFolder.Location = new Point(15, 52);
        lblFolder.AutoSize = true;

        lblFolder.ForeColor =
            Color.FromArgb(190, 190, 195);

        leftPanel.Controls.Add(lblFolder);

        txtFolder = new();

        txtFolder.Location =
            new Point(15, 75);

        txtFolder.Size =
            new Size(220, 27);

        StyleTextBox(txtFolder);

        leftPanel.Controls.Add(txtFolder);

        btnOpenFolder = new();

        btnOpenFolder.Text = "Open";
        btnOpenFolder.Location =
            new Point(242, 74);

        btnOpenFolder.Size =
            new Size(88, 29);

        StyleSecondaryButton(btnOpenFolder);

        btnOpenFolder.Click +=
            btnOpenFolder_Click;

        leftPanel.Controls.Add(btnOpenFolder);

        // =========================================================
        // PRECISION
        // =========================================================

        lblPrecision = new();

        lblPrecision.Text =
            "Detection precision";

        lblPrecision.Location =
            new Point(15, 120);

        lblPrecision.AutoSize = true;

        lblPrecision.ForeColor =
            Color.FromArgb(190, 190, 195);

        leftPanel.Controls.Add(lblPrecision);

        txtPrecision = new();

        txtPrecision.Location =
            new Point(240, 116);

        txtPrecision.Size =
            new Size(90, 27);

        StyleTextBox(txtPrecision);

        leftPanel.Controls.Add(txtPrecision);

        // =========================================================
        // SCAN INTERVAL
        // =========================================================

        lblScan = new();

        lblScan.Text =
            "Scan interval (ms)";

        lblScan.Location =
            new Point(15, 160);

        lblScan.AutoSize = true;

        lblScan.ForeColor =
            Color.FromArgb(190, 190, 195);

        leftPanel.Controls.Add(lblScan);

        txtScanMin = CreateInput(
            leftPanel,
            175,
            156);

        txtScanMax = CreateInput(
            leftPanel,
            265,
            156);

        AddRangeHint(
            leftPanel,
            "Min",
            175,
            184,
            156);

        AddRangeHint(
            leftPanel,
            "Max",
            265,
            274,
            156);

        // =========================================================
        // BEFORE CLICK
        // =========================================================

        lblBeforeClick = new();

        lblBeforeClick.Text =
            "Before click (ms)";

        lblBeforeClick.Location =
            new Point(15, 202);

        lblBeforeClick.AutoSize = true;

        lblBeforeClick.ForeColor =
            Color.FromArgb(190, 190, 195);

        leftPanel.Controls.Add(lblBeforeClick);

        txtBeforeMin = CreateInput(
            leftPanel,
            175,
            198);

        txtBeforeMax = CreateInput(
            leftPanel,
            265,
            198);

        AddRangeHint(
            leftPanel,
            "Min",
            175,
            184,
            198);

        AddRangeHint(
            leftPanel,
            "Max",
            265,
            274,
            198);

        // =========================================================
        // AFTER CLICK
        // =========================================================

        lblAfterClick = new();

        lblAfterClick.Text =
            "After click (ms)";

        lblAfterClick.Location =
            new Point(15, 244);

        lblAfterClick.AutoSize = true;

        lblAfterClick.ForeColor =
            Color.FromArgb(190, 190, 195);

        leftPanel.Controls.Add(lblAfterClick);

        txtAfterMin = CreateInput(
            leftPanel,
            175,
            240);

        txtAfterMax = CreateInput(
            leftPanel,
            265,
            240);

        AddRangeHint(
            leftPanel,
            "Min",
            175,
            184,
            240);

        AddRangeHint(
            leftPanel,
            "Max",
            265,
            274,
            240);

        // =========================================================
        // CLICK AREA
        // =========================================================

        lblClickArea = new();

        lblClickArea.Text =
            "Click area (%)";

        lblClickArea.Location =
            new Point(15, 286);

        lblClickArea.AutoSize = true;

        lblClickArea.ForeColor =
            Color.FromArgb(190, 190, 195);

        leftPanel.Controls.Add(lblClickArea);

        txtClickMin = CreateInput(
            leftPanel,
            175,
            282);

        txtClickMax = CreateInput(
            leftPanel,
            265,
            282);

        AddRangeHint(
            leftPanel,
            "Min",
            175,
            184,
            282);

        AddRangeHint(
            leftPanel,
            "Max",
            265,
            274,
            282);

        // =========================================================
        // BACKGROUND CLICK
        // =========================================================

        chkBackgroundClick = new();

        chkBackgroundClick.Text =
            "Background click";

        chkBackgroundClick.Location =
            new Point(15, 330);

        chkBackgroundClick.AutoSize = true;

        StyleCheckBox(chkBackgroundClick);

        leftPanel.Controls.Add(chkBackgroundClick);

        // =========================================================
        // DEBUG
        // =========================================================

        chkDebug = new();

        chkDebug.Text =
            "Debug mode";

        chkDebug.Location =
            new Point(170, 330);

        chkDebug.AutoSize = true;

        StyleCheckBox(chkDebug);

        chkDebug.ForeColor =
            Color.FromArgb(255, 190, 90);

        leftPanel.Controls.Add(chkDebug);

        Label lblDebugHint = new();

        lblDebugHint.Text =
            "More console output";

        lblDebugHint.Location =
            new Point(170, 353);

        lblDebugHint.AutoSize = true;

        lblDebugHint.Font =
            new Font(
                "Segoe UI",
                7.5F);

        lblDebugHint.ForeColor =
            Color.FromArgb(120, 120, 125);

        leftPanel.Controls.Add(lblDebugHint);

        // =========================================================
        // RIGHT PANEL
        // =========================================================

        Panel rightPanel = new();

        rightPanel.Location =
            new Point(380, 75);

        rightPanel.Size =
            new Size(365, 405);

        rightPanel.BackColor =
            Color.FromArgb(30, 30, 33);

        rightPanel.BorderStyle =
            BorderStyle.FixedSingle;

        Controls.Add(rightPanel);

        // =========================================================
        // TIMING SECTION
        // =========================================================

        Label lblTimingSection = CreateSectionTitle(
            "TIMING",
            new Point(15, 15));

        rightPanel.Controls.Add(lblTimingSection);

        lblBreakEvery = new();

        lblBreakEvery.Text =
            "Break every (minutes)";

        lblBreakEvery.Location =
            new Point(15, 55);

        lblBreakEvery.AutoSize = true;

        lblBreakEvery.ForeColor =
            Color.FromArgb(190, 190, 195);

        rightPanel.Controls.Add(lblBreakEvery);

        txtBreakEveryMin = CreateInput(
            rightPanel,
            205,
            51);

        txtBreakEveryMax = CreateInput(
            rightPanel,
            295,
            51);

        AddRangeHint(
            rightPanel,
            "Min",
            205,
            214,
            51);

        AddRangeHint(
            rightPanel,
            "Max",
            295,
            304,
            51);

        lblBreakDuration = new();

        lblBreakDuration.Text =
            "Break duration (minutes)";

        lblBreakDuration.Location =
            new Point(15, 97);

        lblBreakDuration.AutoSize = true;

        lblBreakDuration.ForeColor =
            Color.FromArgb(190, 190, 195);

        rightPanel.Controls.Add(lblBreakDuration);

        txtBreakDurationMin = CreateInput(
            rightPanel,
            205,
            93);

        txtBreakDurationMax = CreateInput(
            rightPanel,
            295,
            93);

        AddRangeHint(
            rightPanel,
            "Min",
            205,
            214,
            93);

        AddRangeHint(
            rightPanel,
            "Max",
            295,
            304,
            93);

        // =========================================================
        // REGION SECTION
        // =========================================================

        Label lblRegionSection = CreateSectionTitle(
            "DETECTION AREA",
            new Point(15, 150));

        rightPanel.Controls.Add(lblRegionSection);

        btnSelectRegion = new();

        btnSelectRegion.Text =
            "Select Screen Area";

        btnSelectRegion.Location =
            new Point(15, 185);

        btnSelectRegion.Size =
            new Size(160, 38);

        StylePrimaryButton(btnSelectRegion);

        btnSelectRegion.Click +=
            btnSelectRegion_Click;

        rightPanel.Controls.Add(btnSelectRegion);

        btnClearRegion = new();

        btnClearRegion.Text =
            "Full Screen";

        btnClearRegion.Location =
            new Point(185, 185);

        btnClearRegion.Size =
            new Size(145, 38);

        StyleSecondaryButton(btnClearRegion);

        btnClearRegion.Click +=
            btnClearRegion_Click;

        rightPanel.Controls.Add(btnClearRegion);

        Label lblRegionHint = new();

        lblRegionHint.Text =
            "Select an area to reduce the detection search.";

        lblRegionHint.Location =
            new Point(15, 232);

        lblRegionHint.AutoSize = true;

        lblRegionHint.Font =
            new Font(
                "Segoe UI",
                8F);

        lblRegionHint.ForeColor =
            Color.FromArgb(125, 125, 130);

        rightPanel.Controls.Add(lblRegionHint);

        // =========================================================
        // STATUS
        // =========================================================

        Label lblStatus = new();

        lblStatus.Text =
            "READY";

        lblStatus.Location =
            new Point(15, 275);

        lblStatus.AutoSize = true;

        lblStatus.Font =
            new Font(
                "Segoe UI Semibold",
                9F,
                FontStyle.Bold);

        lblStatus.ForeColor =
            Color.FromArgb(100, 200, 130);

        rightPanel.Controls.Add(lblStatus);

        Label lblStatusHint = new();

        lblStatusHint.Text =
            "Configure the detector and press Start.";

        lblStatusHint.Location =
            new Point(15, 300);

        lblStatusHint.AutoSize = true;

        lblStatusHint.Font =
            new Font(
                "Segoe UI",
                8F);

        lblStatusHint.ForeColor =
            Color.FromArgb(125, 125, 130);

        rightPanel.Controls.Add(lblStatusHint);

        // =========================================================
        // CONTROL BAR
        // =========================================================

        Panel controlPanel = new();

        controlPanel.Location =
            new Point(15, 495);

        controlPanel.Size =
            new Size(730, 60);

        controlPanel.BackColor =
            Color.FromArgb(30, 30, 33);

        controlPanel.BorderStyle =
            BorderStyle.FixedSingle;

        Controls.Add(controlPanel);

        btnStart = new();

        btnStart.Text =
            "▶  START";

        btnStart.Location =
            new Point(15, 11);

        btnStart.Size =
            new Size(150, 38);

        StyleStartButton(btnStart);

        btnStart.Click +=
            btnStart_Click;

        controlPanel.Controls.Add(btnStart);

        btnStop = new();

        btnStop.Text =
            "■  STOP";

        btnStop.Location =
            new Point(175, 11);

        btnStop.Size =
            new Size(150, 38);

        StyleStopButton(btnStop);

        btnStop.Enabled = false;

        btnStop.Click +=
            btnStop_Click;

        controlPanel.Controls.Add(btnStop);

        Label lblControlHint = new();

        lblControlHint.Text =
            "The detector runs in the background while scanning the configured screen area.";

        lblControlHint.Location =
            new Point(350, 21);

        lblControlHint.AutoSize = true;

        lblControlHint.Font =
            new Font(
                "Segoe UI",
                8F);

        lblControlHint.ForeColor =
            Color.FromArgb(125, 125, 130);

        controlPanel.Controls.Add(lblControlHint);

        // =========================================================
        // LOG
        // =========================================================

        Label lblLog = new();

        lblLog.Text =
            "ACTIVITY LOG";

        lblLog.Location =
            new Point(17, 575);

        lblLog.AutoSize = true;

        lblLog.Font =
            new Font(
                "Segoe UI Semibold",
                9F,
                FontStyle.Bold);

        lblLog.ForeColor =
            Color.FromArgb(190, 190, 195);

        Controls.Add(lblLog);

        txtLog = new();

        txtLog.Location =
            new Point(15, 600);

        txtLog.Size =
            new Size(730, 145);

        txtLog.ReadOnly = true;

        txtLog.BackColor =
            Color.FromArgb(17, 17, 19);

        txtLog.ForeColor =
            Color.FromArgb(225, 225, 225);

        txtLog.Font =
            new Font(
                "Consolas",
                8.5F);

        txtLog.BorderStyle =
            BorderStyle.FixedSingle;

        txtLog.ScrollBars =
            RichTextBoxScrollBars.Vertical;

        Controls.Add(txtLog);

        ResumeLayout(false);
        PerformLayout();
    }

    // =============================================================
    // HELPERS
    // =============================================================

    private static Label CreateSectionTitle(
        string text,
        Point location)
    {
        Label label = new();

        label.Text = text;
        label.Location = location;
        label.AutoSize = true;

        label.Font =
            new Font(
                "Segoe UI Semibold",
                9F,
                FontStyle.Bold);

        label.ForeColor =
            Color.FromArgb(
                120,
                170,
                255);

        return label;
    }

    private static TextBox CreateInput(
        Control parent,
        int x,
        int y)
    {
        TextBox box = new();

        box.Location =
            new Point(x, y);

        box.Size =
            new Size(80, 27);

        StyleTextBox(box);

        parent.Controls.Add(box);

        return box;
    }

    private static void StyleTextBox(
        TextBox box)
    {
        box.BackColor =
            Color.FromArgb(18, 18, 20);

        box.ForeColor =
            Color.FromArgb(235, 235, 235);

        box.BorderStyle =
            BorderStyle.FixedSingle;

        box.Font =
            new Font(
                "Segoe UI",
                9F);
    }

    private static void StyleCheckBox(
        CheckBox box)
    {
        box.ForeColor =
            Color.FromArgb(205, 205, 210);

        box.BackColor =
            Color.Transparent;

        box.Font =
            new Font(
                "Segoe UI",
                8.5F);

        box.Cursor =
            Cursors.Hand;
    }

    private static void StylePrimaryButton(
        Button button)
    {
        button.BackColor =
            Color.FromArgb(55, 105, 180);

        button.ForeColor =
            Color.White;

        button.FlatStyle =
            FlatStyle.Flat;

        button.FlatAppearance.BorderSize =
            0;

        button.Font =
            new Font(
                "Segoe UI Semibold",
                9F,
                FontStyle.Bold);

        button.Cursor =
            Cursors.Hand;
    }

    private static void StyleSecondaryButton(
        Button button)
    {
        button.BackColor =
            Color.FromArgb(48, 48, 52);

        button.ForeColor =
            Color.FromArgb(220, 220, 225);

        button.FlatStyle =
            FlatStyle.Flat;

        button.FlatAppearance.BorderColor =
            Color.FromArgb(70, 70, 75);

        button.FlatAppearance.BorderSize =
            1;

        button.Font =
            new Font(
                "Segoe UI",
                8.5F);

        button.Cursor =
            Cursors.Hand;
    }

    private static void StyleStartButton(
        Button button)
    {
        button.BackColor =
            Color.FromArgb(45, 145, 85);

        button.ForeColor =
            Color.White;

        button.FlatStyle =
            FlatStyle.Flat;

        button.FlatAppearance.BorderSize =
            0;

        button.Font =
            new Font(
                "Segoe UI Semibold",
                9F,
                FontStyle.Bold);

        button.Cursor =
            Cursors.Hand;
    }

    private static void StyleStopButton(
        Button button)
    {
        button.BackColor =
            Color.FromArgb(145, 55, 55);

        button.ForeColor =
            Color.White;

        button.FlatStyle =
            FlatStyle.Flat;

        button.FlatAppearance.BorderSize =
            0;

        button.Font =
            new Font(
                "Segoe UI Semibold",
                9F,
                FontStyle.Bold);

        button.Cursor =
            Cursors.Hand;
    }

    private static void AddRangeHint(
        Control parent,
        string text,
        int x,
        int labelX,
        int y)
    {
        Label label = new();

        label.Text = text;

        label.Location =
            new Point(
                labelX,
                y + 5);

        label.AutoSize = true;

        label.Font =
            new Font(
                "Segoe UI",
                7F);

        label.ForeColor =
            Color.FromArgb(
                105,
                105,
                110);

        parent.Controls.Add(label);
    }
}