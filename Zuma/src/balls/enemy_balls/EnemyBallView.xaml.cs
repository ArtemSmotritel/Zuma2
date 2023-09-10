using System.Windows.Controls;

namespace Zuma.src.balls.enemy_balls
{
    /// <summary>
    /// Interaction logic for EnemyBallView.xaml
    /// </summary>
    public partial class EnemyBallView : UserControl
    {
        public EnemyBallViewModel ViewModel;

        public EnemyBallView(EnemyBallViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            DataContext = ViewModel;
        }
    }
}
