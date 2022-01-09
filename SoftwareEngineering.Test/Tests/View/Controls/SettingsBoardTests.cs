using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;
using SoftwareEngineering.View.Controls.SettingsBoard;

namespace SoftwareEngineering.Test.Tests.View.Controls
{
    [TestFixture]
    public class SettingsBoardTests
    {
        private SettingsBoardViewModel Init(string mapWidth, string mapHeight, string lengthToWin, Mark firstMark)
        {
            return new SettingsBoardViewModel(
                () => mapWidth,
                () => mapHeight,
                () => lengthToWin,
                () => firstMark);
        }

        private const string MapWidthString = "10";
        private const string MapHeightString = "10";
        private const string LengthToWinString = "5";
        private const Mark FirstMark = Mark.Cross;

        [Test(Description = "Тестирование получения настроек при различных входных данных")]
        [TestCase("",             "",              "",                Mark.None, false)]
        [TestCase("",             "",              "",                FirstMark, false)]
        [TestCase("",             "",              LengthToWinString, Mark.None, false)]
        [TestCase("",             "",              LengthToWinString, FirstMark, false)]
        //
        [TestCase("",             MapHeightString, "",                Mark.None, false)]
        [TestCase("",             MapHeightString, "",                FirstMark, false)]
        [TestCase("",             MapHeightString, LengthToWinString, Mark.None, false)]
        [TestCase("",             MapHeightString, LengthToWinString, FirstMark, false)]
        //
        [TestCase(MapWidthString, "",              "",                Mark.None, false)]
        [TestCase(MapWidthString, "",              "",                FirstMark, false)]
        [TestCase(MapWidthString, "",              LengthToWinString, Mark.None, false)]
        [TestCase(MapWidthString, "",              LengthToWinString, FirstMark, false)]
        //
        [TestCase(MapWidthString, MapHeightString, "",                Mark.None, false)]
        [TestCase(MapWidthString, MapHeightString, "",                FirstMark, false)]
        [TestCase(MapWidthString, MapHeightString, LengthToWinString, Mark.None, false)]
        [TestCase(MapWidthString, MapHeightString, LengthToWinString, FirstMark, true )]
        public void GetSettingsTest(string width, string height, string lengthToWin, Mark firstMark, bool expected)
        {
            SettingsBoardViewModel board = Init(width, height, lengthToWin, firstMark);

            GameSettings settings = board.GetSettings(out string errorMessage);
            
            Assert.AreEqual(expected, settings != null, "Ожидалось корректное заполнение настроек");
            Assert.AreEqual(expected, string.IsNullOrWhiteSpace(errorMessage), "Ожидалось отсутсвие сообщения об ошибке");

            if (settings != null)
            {
                Assert.AreEqual(settings.MapWidth, Convert.ToUInt32(width), "Ширина поля искажена");
                Assert.AreEqual(settings.MapHeight, Convert.ToUInt32(height), "Высота поля искажена");
                Assert.AreEqual(settings.LengthToWin, Convert.ToUInt32(lengthToWin), "Длина серии для выигрыша искажена");
                Assert.AreEqual(settings.FirstMark, firstMark, "Очередность ходов искажена");
            }
        }
    }
}
