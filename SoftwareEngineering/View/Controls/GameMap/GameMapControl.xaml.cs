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
            ViewModel = new GameMapViewModel(map => Redraw(map, MyGrid));
        }

        private void Redraw(Mark[,] map, Grid grid)
        {
            Clear(grid);
            InitializeGrid(grid, map.GetLength(0), map.GetLength(1));
            FillGrid(map, grid);
        }

        private void Clear(Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
        }

        private void InitializeGrid(Grid grid, int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30)});

            for (int i = 0; i < columnCount; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
        }

        private void FillGrid(Mark[,] map, Grid grid)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int row = i;
                    int column = j;
                    Button button = new Button()
                    {
                        Content = GetText(map[row, column]),
                    };
                    button.Click += (s, e) => ViewModel.MapClicked(row, column);

                    grid.Children.Add(button);
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                }
            }
        }

        private string GetText(Mark mark)
        {
            return mark.GetValueByMark("", "O", "Х");
        }
    }
}
