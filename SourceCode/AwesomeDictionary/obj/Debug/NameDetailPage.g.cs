#pragma checksum "D:\KodServer\VisualStudioProjeleri\AwesomeDictionary\AwesomeDictionary\NameDetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "85692D28342CD5019F57FD27A647FF69"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace AwesomeDictionary {
    
    
    public partial class NameDetailPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Style MyTextBlockStyle;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock lblApp;
        
        internal System.Windows.Controls.TextBlock lblNameDetail;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ScrollViewer svMeaning;
        
        internal System.Windows.Controls.TextBox txtMeaning;
        
        internal System.Windows.Controls.Grid pnlKeyboardPlaceHolder;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Awesome%20Dictionary;component/NameDetailPage.xaml", System.UriKind.Relative));
            this.MyTextBlockStyle = ((System.Windows.Style)(this.FindName("MyTextBlockStyle")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.lblApp = ((System.Windows.Controls.TextBlock)(this.FindName("lblApp")));
            this.lblNameDetail = ((System.Windows.Controls.TextBlock)(this.FindName("lblNameDetail")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.svMeaning = ((System.Windows.Controls.ScrollViewer)(this.FindName("svMeaning")));
            this.txtMeaning = ((System.Windows.Controls.TextBox)(this.FindName("txtMeaning")));
            this.pnlKeyboardPlaceHolder = ((System.Windows.Controls.Grid)(this.FindName("pnlKeyboardPlaceHolder")));
        }
    }
}

