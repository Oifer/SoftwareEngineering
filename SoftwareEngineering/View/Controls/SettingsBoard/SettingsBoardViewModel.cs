using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        private readonly Func<string> _getWidthFunc;
        private readonly Func<string> _getHeightFunc;
        private readonly Func<string> _getLengthToWinFunc;
        private readonly Func<Mark> _getFirstMarkFunc;
    }
}
