﻿#pragma checksum "C:\code\MvvmCrossConference-master\Cirrious.Conference.UI.WP7\Controls\ExtendedListBox.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D67FC69DB41443B9DAEFC509F452EC64"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Cirrious.Conference.UI.WP7.Controls {
    
    
    public partial class ExtendedListBox : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.StackPanel stackPanelAddToHead;
        
        internal System.Windows.Controls.TextBlock textBlockAddToHead;
        
        internal System.Windows.Controls.ListBox UnderlyingListBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Cirrious.Conference.UI.WP7;component/Controls/ExtendedListBox.xaml", System.UriKind.Relative));
            this.stackPanelAddToHead = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanelAddToHead")));
            this.textBlockAddToHead = ((System.Windows.Controls.TextBlock)(this.FindName("textBlockAddToHead")));
            this.UnderlyingListBox = ((System.Windows.Controls.ListBox)(this.FindName("UnderlyingListBox")));
        }
    }
}

