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
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Controls.GameMap
{
    /// <summary>
    /// Логика взаимодействия для GameMapControl.xaml
    /// </summary>
    public partial class GameMapControl : UserControl
    {
        public readonly GameMapViewModel ViewModel;

        public GameMapControl()
        {
            InitializeComponent();
            ViewModel = new GameMapViewModel();
        }
    }
}
