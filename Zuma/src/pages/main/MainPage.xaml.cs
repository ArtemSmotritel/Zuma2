using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Zuma.src.level;
using Zuma.src.level_creators;
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
            Levels = CreateLevels();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new LevelSelectionPage(Levels));

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

        private List<Level> CreateLevels()
        {
            return new List<Level>
            {
                new FirstLevelCreator().Create()
            };
        }
    }
}
