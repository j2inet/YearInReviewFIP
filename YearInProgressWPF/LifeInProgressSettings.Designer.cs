﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YearInProgressWPF {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
    internal sealed partial class LifeInProgressSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static LifeInProgressSettings defaultInstance = ((LifeInProgressSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new LifeInProgressSettings())));
        
        public static LifeInProgressSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("192, 192, 255")]
        public global::System.Drawing.Color ForegroundColor {
            get {
                return ((global::System.Drawing.Color)(this["ForegroundColor"]));
            }
            set {
                this["ForegroundColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Maroon")]
        public global::System.Drawing.Color BackgroundColor {
            get {
                return ((global::System.Drawing.Color)(this["BackgroundColor"]));
            }
            set {
                this["BackgroundColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("192, 192, 255")]
        public global::System.Drawing.Color TextColor {
            get {
                return ((global::System.Drawing.Color)(this["TextColor"]));
            }
            set {
                this["TextColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("90")]
        public float AnticipatedLifeSpan {
            get {
                return ((float)(this["AnticipatedLifeSpan"]));
            }
            set {
                this["AnticipatedLifeSpan"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1978-03-28")]
        public global::System.DateTime BirthDate {
            get {
                return ((global::System.DateTime)(this["BirthDate"]));
            }
            set {
                this["BirthDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Have you accomplished anythign yet?")]
        public string Tag {
            get {
                return ((string)(this["Tag"]));
            }
            set {
                this["Tag"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("24.00:00:00")]
        public global::System.TimeSpan RefreshInterval {
            get {
                return ((global::System.TimeSpan)(this["RefreshInterval"]));
            }
            set {
                this["RefreshInterval"] = value;
            }
        }
    }
}