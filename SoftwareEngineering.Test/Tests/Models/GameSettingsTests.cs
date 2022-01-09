using NUnit.Framework;
using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.Test.Tests.Models
{
    [TestFixture]
    public class GameSettingsTests
    {
        private GameSettings Init() => new GameSettings();

        [Test(Description = "Проверка свойства ширины поля")]
        public void MapWidthTest()
        {
            const uint width = 5;

            GameSettings settings = Init();

            settings.MapWidth = width;
            Assert.AreEqual(width, settings.MapWidth, "Свойство не присваивается");
        }

        [Test(Description = "Проверка свойства высоты поля")]
        public void MapHeightTest()
        {
            const uint height = 5;

            GameSettings settings = Init();

            settings.MapHeight = height;
            Assert.AreEqual(height, settings.MapHeight, "Свойство не присваивается");
        }

        [Test(Description = "Проверка свойства длины серии для выигрыша")]
        public void LengthToWinTest()
        {
            const uint length = 5;

            GameSettings settings = Init();

            settings.LengthToWin = length;
            Assert.AreEqual(length, settings.LengthToWin, "Свойство не присваивается");
        }

        [Test(Description = "Проверка свойства очередности ходов")]
        public void FirstMarkTest()
        {
            const Mark mark = Mark.Naught;

            GameSettings settings = Init();

            settings.FirstMark = mark;
            Assert.AreEqual(mark, settings.FirstMark, "Свойство не присваивается");
        }
    }
}
