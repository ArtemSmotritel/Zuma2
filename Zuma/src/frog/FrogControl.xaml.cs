using System.Windows.Controls;
using System.Windows.Media;

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

            DataContext = ViewModel;
        }
        public void SetRotationAngle(double angle) => ( (RotateTransform) RenderTransform ).Angle = angle;
    }
}
