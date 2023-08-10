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
using Zuma.src.models.level;

namespace Zuma.src.pages.level
{
    /// <summary>
    /// Interaction logic for LevelPage.xaml
    /// </summary>
    public partial class LevelPage : Page
    {
        private Level Level { get; set; }

        public LevelPage()
        {
            InitializeComponent();
        }

        public LevelPage(Level level) : this()
        {
            Level = level;
        }
    }
}
