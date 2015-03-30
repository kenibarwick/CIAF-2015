﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cirrious.MvvmCross.Interfaces.Commands;

namespace Cirrious.Conference.UI.WP7.Controls
{
    public partial class IconWithTextMenuItem : UserControl
    {
        public IconWithTextMenuItem()
        {
            InitializeComponent();
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IconWithTextMenuItem), new PropertyMetadata(null, OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parent = (IconWithTextMenuItem)d;
            parent.MainText.Text = (string)e.NewValue;
        }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(IconWithTextMenuItem), new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parent = (IconWithTextMenuItem)d;
            parent.IconImage.Source = (ImageSource)e.NewValue;
        }

        public Style TextStyle
        {
          get { return (Style)GetValue(TextStyleProperty); }
          set { SetValue(TextStyleProperty, value); }
        }

        public static readonly DependencyProperty TextStyleProperty =
            DependencyProperty.Register("TextStyle", typeof(Style), typeof(IconWithTextMenuItem), new PropertyMetadata(OnTextStyleChanged));

        private static void OnTextStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          var parent = (IconWithTextMenuItem)d;
          parent.MainText.Style = (Style)e.NewValue;
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(IconWithTextMenuItem), new PropertyMetadata(null));

        public IMvxCommand Command
        {
            get { return (IMvxCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(IMvxCommand), typeof(IconWithTextMenuItem), new PropertyMetadata(null));

        private void OnTap(object sender, GestureEventArgs e)
        {
            if (Command == null)
                return;

            if (CommandParameter != null)
                Command.Execute(CommandParameter);
            else
                Command.Execute();
        }
    }
}
