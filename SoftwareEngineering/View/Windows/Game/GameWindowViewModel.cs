using System;

using SoftwareEngineering.Models;

namespace SoftwareEngineering.View.Windows.Game
{
    /// <summary> Тип, реализующий логику экрана игры </summary>
    public class GameWindowViewModel : IGameWindow
    {
        public GameWindowViewModel(
            Func<ResponseWithErrorMessage<GameSettings>> getSettingsFunc,
            Action<string> messageBoxAction,
            Action<GameSettings> startGameAction,
            Action clearMapAction)
        {
            _getSettingsFunc = getSettingsFunc;
            _messageBoxAction = messageBoxAction;
            _startGameAction = startGameAction;
            _clearMapAction = clearMapAction;
        }

        public void StartGame()
        {
            ResponseWithErrorMessage<GameSettings> response = _getSettingsFunc();
            if (response.IsCorrect)
                _startGameAction(response.Response);
            else
                _messageBoxAction(response.ErrorMessage);
        }

        public void Clear()
        {
            _clearMapAction();
        }

        private readonly Func<ResponseWithErrorMessage<GameSettings>> _getSettingsFunc;
        private readonly Action<string> _messageBoxAction;
        private readonly Action<GameSettings> _startGameAction;
        private readonly Action _clearMapAction;
    }
}
