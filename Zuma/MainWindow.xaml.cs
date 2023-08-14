﻿using System.Windows;
using Zuma.src.pages.main;

namespace Zuma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new MainPage());
        }
    }
}
