using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Zuma.src.models.level;
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
            Levels = new List<Level> { new Level("The simplest", 1), new Level("The midiumest", 2), new Level("The hardest", 3) };
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LevelSelectionPage(Levels));
        }
    }
}
