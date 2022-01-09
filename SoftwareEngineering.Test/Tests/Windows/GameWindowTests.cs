using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;
using SoftwareEngineering.View.Windows.Game;

namespace SoftwareEngineering.Test.Tests.Windows
{
    [TestFixture]
    public class GameWindowTests
    {
        private GameWindowViewModel Init(
            ResponseWithErrorMessage<GameSettings> settingsResponse = null,
            Action<string> messageBoxAction = null,
            Action<GameSettings> startGameAction = null,
            Action clearMapAction = null)
        {
            settingsResponse = settingsResponse ?? new ResponseWithErrorMessage<GameSettings>("");
            messageBoxAction = messageBoxAction ?? (_ => { });
            startGameAction = startGameAction ?? (_ => { });
            clearMapAction = clearMapAction ?? (() => { });

            return new GameWindowViewModel(() => settingsResponse, messageBoxAction, startGameAction, clearMapAction);
        }

        private static readonly ResponseWithErrorMessage<GameSettings> CorrectSettings =
            new ResponseWithErrorMessage<GameSettings>(new GameSettings(10, 10, 5, Mark.Cross));

        private static readonly ResponseWithErrorMessage<GameSettings> IncorrectSettings =
            new ResponseWithErrorMessage<GameSettings>("Ошибка");

        [Test(Description = "Тестирование запуска игры при различных настройках")]
        [TestCase(false, false)]
        [TestCase(true , true )]
        public void StartGameTest(bool areSettingsCorrect, bool expected)
        {
            GameSettings settingsToStart = null;
            GameWindowViewModel viewModel = Init(settingsResponse: areSettingsCorrect ? CorrectSettings : IncorrectSettings,
                                                 startGameAction: settings => settingsToStart = settings);

            viewModel.StartGame();

            Assert.AreEqual(expected, settingsToStart != null);
            if (settingsToStart != null)
                Assert.AreEqual(CorrectSettings.Response, settingsToStart);
        }

        [Test(Description = "Тестирование очистки поля")]
        public void ClearTest()
        {
            bool clearMapCalled = false;
            GameWindowViewModel viewModel = Init(clearMapAction: () => clearMapCalled = true);

            viewModel.Clear();
            
            Assert.IsTrue(clearMapCalled);
        }
    }
}
