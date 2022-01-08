using System;
using System.Windows.Controls;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Controls.GameMap
{
    public class GameMapViewModel : IGameMap
    {
        public GameMapViewModel()
        { }

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

        protected Mark[,] _map = new Mark[,] { };

        private readonly Action<Grid> _onRedraw;
    }
}
