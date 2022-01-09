using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareEngineering.Models;

namespace SoftwareEngineering.View.Controls.SettingsBoard
{
    public interface ISettingsBoard
    {
        /// <summary> Получение текущих настроек </summary>
        /// <param name="errorMessage">сообщение об ошибке в случае ввода некорректных данных</param>
        /// <returns>Введенные данные или null, если некорректны</returns>
        GameSettings GetSettings(out string errorMessage);
    }
}
