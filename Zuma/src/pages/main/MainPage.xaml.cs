using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Zuma.src.controllers;
using Zuma.src.level;
using Zuma.src.pages.level_selection;

namespace Zuma.src.pages.main
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<Level> Levels { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Levels = new List<Level> {
                LevelCreator.CreateFirstLevel()
            };
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new LevelSelectionPage(Levels));
    }
}
