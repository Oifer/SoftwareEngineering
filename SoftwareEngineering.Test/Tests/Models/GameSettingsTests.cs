using NUnit.Framework;
using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.Test.Tests.Models
{
    [TestFixture]
    public class GameSettingsTests
    {
        private GameSettings Init(
            uint mapWidth = 10, 
            uint mapHeight = 10, 
            uint lengthToWin = 5, 
            Mark firstMark = Mark.Cross) => new GameSettings(mapWidth, mapHeight, lengthToWin, firstMark);

        [Test(Description = "Проверка свойства ширины поля")]
        public void MapWidthTest()
        {
            const uint width = 5;

            GameSettings settings = Init(mapWidth: width);
            
            Assert.AreEqual(width, settings.MapWidth, "Свойство не присваивается");
        }

        [Test(Description = "Проверка свойства высоты поля")]
        public void MapHeightTest()
        {
            const uint height = 5;

            GameSettings settings = Init(mapHeight: height);
            
            Assert.AreEqual(height, settings.MapHeight, "Свойство не присваивается");
        }

        [Test(Description = "Проверка свойства длины серии для выигрыша")]
        public void LengthToWinTest()
        {
            const uint length = 5;

            GameSettings settings = Init(lengthToWin: length);
            
            Assert.AreEqual(length, settings.LengthToWin, "Свойство не присваивается");
        }

        [Test(Description = "Проверка свойства очередности ходов")]
        public void FirstMarkTest()
        {
            const Mark mark = Mark.Naught;

            GameSettings settings = Init(firstMark: mark);
            
            Assert.AreEqual(mark, settings.FirstMark, "Свойство не присваивается");
        }
    }
}
