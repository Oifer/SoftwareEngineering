using SoftwareEngineering.Models;

namespace SoftwareEngineering.View.Controls.SettingsBoard
{
    public interface ISettingsBoard
    {
        /// <summary> Получение текущих настроек </summary>
        ResponseWithErrorMessage<GameSettings> GetSettings();
    }
}
