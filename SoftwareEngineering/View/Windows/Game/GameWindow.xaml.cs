using System;
using System.Windows;
using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Windows.Game
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly GameWindowViewModel ViewModel;

        public GameWindow()
        {
            InitializeComponent();
            ViewModel = new GameWindowViewModel(
                SettingsBoard.ViewModel.GetSettings,
                ShowMessage,
                StartGame,
                ClearMap);

            GameMap.ViewModel.WinEvent += OnWin;
            GameMap.ViewModel.NoEmptyCellsEvent += OnNoEmptyCells;
            GameMap.ViewModel.NextMarkChanged += _ => WriteCurrentTurn();

            GameMap.IsEnabled = false;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void OnStartGameClicked(object sender, EventArgs e)
        {
            ViewModel.StartGame();
        }

        private void OnClearMapClicked(object sender, EventArgs e)
        {
            ViewModel.Clear();
        }

        private void StartGame(GameSettings settings)
        {
            ClearMap();
            
            GameMap.ViewModel.MapWidth = settings.MapWidth;
            GameMap.ViewModel.MapHeight = settings.MapHeight;
            GameMap.ViewModel.LengthToWin = settings.LengthToWin;
            GameMap.ViewModel.NextMark = settings.FirstMark;
            
            EnableMap();
        }

        private void OnWin(Mark winner)
        {
            string name = winner.GetValueByMark("", "Нолики", "Крестики");

            MessageBox.Show(name + " выиграли");
            DisableMap();
        }

        private void OnNoEmptyCells()
        {
            MessageBox.Show("Ничья. Не осталось пустых клеток");
            DisableMap();
        }

        private void EnableMap()
        {
            GameMap.IsEnabled = true;
            WriteCurrentTurn();
        }

        private void DisableMap()
        {
            GameMap.IsEnabled = false;
            WriteCurrentTurn();
        }

        private void WriteCurrentTurn()
        {
            if (!GameMap.IsEnabled)
                CurrentTurnLabel.Content = "";
            else
                CurrentTurnLabel.Content = "Следующий ход: " +
                                           GameMap.ViewModel.NextMark.GetValueByMark("", "нолики", "крестики");
        }

        private void ClearMap()
        {
            GameMap.ViewModel.Clear();
        }
    }
}
