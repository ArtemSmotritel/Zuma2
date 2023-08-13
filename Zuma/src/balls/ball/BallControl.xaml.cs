using System.Windows.Controls;

namespace Zuma.src.balls.ball
{
    /// <summary>
    /// Interaction logic for BallControl.xaml
    /// </summary>
    public partial class BallControl : UserControl
    {
        public BallViewModel ViewModel { get; private set; }

        public BallControl(BallViewModel ballViewModel)
        {
            InitializeComponent();

            ViewModel = ballViewModel;

            DataContext = ViewModel;
        }
    }
}
