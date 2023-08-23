using System.Windows.Controls;

namespace Zuma.src.frog
{
    /// <summary>
    /// Interaction logic for FrogControl.xaml
    /// </summary>
    public partial class FrogControl : UserControl
    {
        public FrogViewModel ViewModel { get; private set; }

        public FrogControl(FrogViewModel frogViewModel)
        {
            InitializeComponent();

            ViewModel = frogViewModel;
            ViewModel.CurrentBallView = CurrentBallSprite;

            DataContext = ViewModel;
        }
    }
}
