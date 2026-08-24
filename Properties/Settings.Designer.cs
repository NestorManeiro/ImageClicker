namespace ImageClicker.Properties
{
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(
        "Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator",
        "1.0.0.0")]
    internal sealed partial class Settings :
        global::System.Configuration.ApplicationSettingsBase
    {
        private static Settings defaultInstance =
            ((Settings)(
                global::System.Configuration.ApplicationSettingsBase.Synchronized(
                    new Settings())));

        public static Settings Default
        {
            get => defaultInstance;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("Images")]
        public string ImageFolder
        {
            get => (string)this["ImageFolder"];
            set => this["ImageFolder"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("0.88")]
        public double Threshold
        {
            get => (double)this["Threshold"];
            set => this["Threshold"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("100")]
        public int ScanMin
        {
            get => (int)this["ScanMin"];
            set => this["ScanMin"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("300")]
        public int ScanMax
        {
            get => (int)this["ScanMax"];
            set => this["ScanMax"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("100")]
        public int BeforeClickMin
        {
            get => (int)this["BeforeClickMin"];
            set => this["BeforeClickMin"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("200")]
        public int BeforeClickMax
        {
            get => (int)this["BeforeClickMax"];
            set => this["BeforeClickMax"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("500")]
        public int AfterClickMin
        {
            get => (int)this["AfterClickMin"];
            set => this["AfterClickMin"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("1000")]
        public int AfterClickMax
        {
            get => (int)this["AfterClickMax"];
            set => this["AfterClickMax"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("15")]
        public int ClickMarginMin
        {
            get => (int)this["ClickMarginMin"];
            set => this["ClickMarginMin"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("85")]
        public int ClickMarginMax
        {
            get => (int)this["ClickMarginMax"];
            set => this["ClickMarginMax"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("20")]
        public int BreakEveryMin
        {
            get => (int)this["BreakEveryMin"];
            set => this["BreakEveryMin"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("30")]
        public int BreakEveryMax
        {
            get => (int)this["BreakEveryMax"];
            set => this["BreakEveryMax"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("1")]
        public int BreakDurationMin
        {
            get => (int)this["BreakDurationMin"];
            set => this["BreakDurationMin"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("2")]
        public int BreakDurationMax
        {
            get => (int)this["BreakDurationMax"];
            set => this["BreakDurationMax"] = value;
        }

        [global::System.Configuration.UserScopedSetting()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Configuration.DefaultSettingValue("true")]
        public bool BackgroundClick
        {
            get => (bool)this["BackgroundClick"];
            set => this["BackgroundClick"] = value;
        }
    }
}