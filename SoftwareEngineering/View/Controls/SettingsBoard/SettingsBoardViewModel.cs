using System;

using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Controls.SettingsBoard
{
    /// <summary>
    /// Тип, реализующий логику панели настроек
    /// </summary>
    public class SettingsBoardViewModel : ISettingsBoard
    {
        public SettingsBoardViewModel(
            Func<string> getWidthFunc,
            Func<string> getHeightFunc,
            Func<string> getLengthToWinFunc,
            Func<Mark> getFirstMarkFunc)
        {
            _getWidthFunc = getWidthFunc;
            _getHeightFunc = getHeightFunc;
            _getLengthToWinFunc = getLengthToWinFunc;
            _getFirstMarkFunc = getFirstMarkFunc;
        }

        /// <inheritdoc />
        public GameSettings GetSettings(out string errorMessage)
        {
            Mark mark = _getFirstMarkFunc();

            //при прождении проверок все переменные получат значения
            uint mapWidth = 0, mapHeight = 0, lengthToWin = 0;
            errorMessage = string.Empty;

            if (!uint.TryParse(_getWidthFunc(), out mapWidth) || mapWidth == 0)
                errorMessage = "Некорректная ширина поля";
            else if (!uint.TryParse(_getHeightFunc(), out mapHeight) || mapHeight == 0)
                errorMessage = "Некорректная высота поля";
            else if (!uint.TryParse(_getLengthToWinFunc(), out lengthToWin) || lengthToWin == 0)
                errorMessage = "Некорректная длина серии для выигрыша";
            else if (mark == Mark.None)
                errorMessage = "Некорректная очередность ходов";

            if (!string.IsNullOrWhiteSpace(errorMessage))
                return null;

            return new GameSettings(mapWidth, mapHeight, lengthToWin, mark);
        }

        private readonly Func<string> _getWidthFunc;
        private readonly Func<string> _getHeightFunc;
        private readonly Func<string> _getLengthToWinFunc;
        private readonly Func<Mark> _getFirstMarkFunc;
    }
}
