using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zuma.src.pages.level_selection
{
    /// <summary>
    /// Interaction logic for LevelSelectionPage.xaml
    /// </summary>
    public partial class LevelSelectionPage : Page
    {
        public LevelSelectionPage()
        {
            InitializeComponent();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
