using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Zuma.src.models.level;
using Zuma.src.pages.level;
using Zuma.src.utils;

namespace Zuma.src.pages.level_selection
{
    /// <summary>
    /// Interaction logic for LevelSelectionPage.xaml
    /// </summary>
    public partial class LevelSelectionPage : Page
    {
        private List<Level> Levels { get; set; }

        readonly GridLengthConverter gridLengthConverter = new GridLengthConverter();

        public LevelSelectionPage()
        {
            InitializeComponent();
            Levels = Enumerable.Empty<Level>().ToList();
        }

        public LevelSelectionPage(List<Level> levels) : this()
        {
            Levels = levels;
            InitializeLevels();
        }

        private void InitializeLevels()
        {
            LevelsGrid.RowDefinitions.Add(new RowDefinition());

            int columnIndex = 0;
            foreach (Level level in Levels)
            {
                LevelsGrid.ColumnDefinitions.Add(new ColumnDefinition());

                var button = new Button
                {
                    Content = level.Number.ToString(),
                    Height = 100,
                    Width = 100,
                    FontSize = 40,
                    CommandParameter = level,
                    Command = new RelayCommand(param => HandleLevelButtonClick((Level) param)),
                };
                button.SetValue(Grid.ColumnProperty, columnIndex);
                button.SetValue(Grid.RowProperty, 0);
                
                LevelsGrid.Children.Add(button);
                
                columnIndex++;
            }

            var finalRow = new RowDefinition();
            finalRow.Height = (GridLength) gridLengthConverter.ConvertFrom("*");
            LevelsGrid.RowDefinitions.Add(finalRow);
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void HandleLevelButtonClick(Level level)
        {
            NavigationService.Navigate(new LevelPage(level));
        }

        private void Page_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                GoBack();
            }
        }

        private void GoBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                GoBack();
            }
        }
    }
}
