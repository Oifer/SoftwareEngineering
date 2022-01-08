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
    public partial class GameMapControl : UserControl, IGameMap
    {
        public GameMapControl()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        public int MapWidth { get; set; }

        /// <inheritdoc />
        public int MapHeight { get; set; }

        /// <inheritdoc />
        public int LengthToWin { get; set; }

        /// <inheritdoc />
        public Mark NextMark { get; set; }

        /// <inheritdoc />
        public event Action<Mark> WinEvent;

        /// <inheritdoc />
        public event Action NoEmptyCellsEvent;

        /// <inheritdoc />
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
