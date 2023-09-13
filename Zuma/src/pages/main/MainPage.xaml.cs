using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Zuma.src.level_creators;
using Zuma.src.pages.level_selection;

namespace Zuma.src.pages.main
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<LevelCreator> LevelCreators { get; set; }

        public MainPage()
        {
            InitializeComponent();
            LevelCreators = CreateLevelCreators();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new LevelSelectionPage(LevelCreators));

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You Serious?",
                                          "You are about to close the game",
                                          MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private List<LevelCreator> CreateLevelCreators()
        {
            return new List<LevelCreator>
            {
                new FirstLevelCreator(),
                new SecondLevelCreator(),
            };
        }
    }
}
