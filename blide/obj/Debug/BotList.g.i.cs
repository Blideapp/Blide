#pragma checksum "..\..\Botlist.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "66D3E2CB7D46FF68F1D53A3A65B734E6837D93854BC04745E83E0C94C41F79E0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Blide;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Blide {
    
    
    /// <summary>
    /// Botlist
    /// </summary>
    public partial class Botlist : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Blide.ToggleButton Bu2;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Blide.ToggleButton Bu1;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExtractButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveAsButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadClipboardButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox usernames;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button replaceButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox toReplace;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox replacement;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Botlist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveImportButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Blide;component/botlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Botlist.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Bu2 = ((Blide.ToggleButton)(target));
            return;
            case 2:
            this.Bu1 = ((Blide.ToggleButton)(target));
            return;
            case 3:
            this.ExtractButton = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\Botlist.xaml"
            this.ExtractButton.Click += new System.Windows.RoutedEventHandler(this.ExtractButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SaveAsButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\Botlist.xaml"
            this.SaveAsButton.Click += new System.Windows.RoutedEventHandler(this.SaveAsButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LoadClipboardButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\Botlist.xaml"
            this.LoadClipboardButton.Click += new System.Windows.RoutedEventHandler(this.LoadClipboardButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.usernames = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.replaceButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Botlist.xaml"
            this.replaceButton.Click += new System.Windows.RoutedEventHandler(this.replaceButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.toReplace = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.replacement = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.SaveImportButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Botlist.xaml"
            this.SaveImportButton.Click += new System.Windows.RoutedEventHandler(this.SaveImportButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

