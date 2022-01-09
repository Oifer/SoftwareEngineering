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
        public ResponseWithErrorMessage<GameSettings> GetSettings()
        {
            Mark mark = _getFirstMarkFunc();
            

            if (!uint.TryParse(_getWidthFunc(), out uint mapWidth) || mapWidth == 0)
                return new ResponseWithErrorMessage<GameSettings>("Некорректная ширина поля");

            if (!uint.TryParse(_getHeightFunc(), out uint mapHeight) || mapHeight == 0)
                return new ResponseWithErrorMessage<GameSettings>("Некорректная высота поля");

            if (!uint.TryParse(_getLengthToWinFunc(), out uint lengthToWin) || lengthToWin == 0)
                return new ResponseWithErrorMessage<GameSettings>("Некорректная длина серии для выигрыша");

            if (mark == Mark.None)
                return new ResponseWithErrorMessage<GameSettings>("Некорректная очередность ходов");
            
            return new ResponseWithErrorMessage<GameSettings>(new GameSettings(mapWidth, mapHeight, lengthToWin, mark));
        }

        private readonly Func<string> _getWidthFunc;
        private readonly Func<string> _getHeightFunc;
        private readonly Func<string> _getLengthToWinFunc;
        private readonly Func<Mark> _getFirstMarkFunc;
    }
}
