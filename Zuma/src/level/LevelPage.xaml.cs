using System.Windows.Controls;

namespace Zuma.src.level
{
    /// <summary>
    /// Interaction logic for LevelPage.xaml
    /// </summary>
    public partial class LevelPage : Page
    {
        public LevelViewModel LevelViewModel { get; private set; }

        public LevelPage(LevelViewModel levelViewModel)
        {
            InitializeComponent();

            LevelViewModel = levelViewModel;

            DataContext = LevelViewModel;
        }
    }
}
