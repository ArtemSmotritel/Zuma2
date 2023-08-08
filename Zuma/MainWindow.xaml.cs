using System;
using System.Windows;
using System.Windows.Threading;
using Zuma.src.pages.main;

namespace Zuma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private int tick = 0;

        public MainWindow()
        {
            InitializeComponent();

            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start();

            MainFrame.Navigate(new MainPage());
        }

        private void GameTick(object sender, EventArgs e)
        {
            block.Text = tick.ToString();
            tick++;
        }
    }
}
