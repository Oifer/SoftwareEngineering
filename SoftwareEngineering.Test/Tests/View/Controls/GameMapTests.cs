using System.Threading.Tasks;

using NUnit.Framework;

using SoftwareEngineering.Models.Enums;
using SoftwareEngineering.View.Controls.GameMap;

using SoftwareEngineering.Test.TestObjects.View.Controls;

namespace SoftwareEngineering.Test.Tests.View.Controls
{
    [TestFixture]
    public class GameMapTests
    {
        private GameMapViewModel Init(out GameMapTestObject testObject)
        {
            testObject = new GameMapTestObject();
            return (GameMapViewModel) testObject;
        }

        [Test(Description = "Тестирование создания объекта")]
        public void InitializationTest()
        {
            GameMapTestObject testObject;
            Init(out testObject);
            Assert.Pass();
        }

        [Test(Description = "Тестирование изменения ширины поля")]
        public void WidthChangeTest()
        {
            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            uint oldWidth = map.MapWidth;
            uint newWidth = oldWidth + 2;

            map.MapWidth = newWidth;

            Assert.AreEqual(newWidth, testObject.Map.GetLength(1), "Фактическая ширина не изменилась");
            Assert.AreEqual(newWidth, map.MapWidth, "Свойство ширины поля не изменилось");
        }

        [Test(Description = "Тестирование изменения высоты поля")]
        public void HeightChangeTest()
        {
            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            uint oldHeight = map.MapHeight;
            uint newHeight = oldHeight + 2;

            map.MapHeight = newHeight;

            Assert.AreEqual(newHeight, testObject.Map.GetLength(0), "Фактическая высота поля не изменилась");
            Assert.AreEqual(newHeight, map.MapHeight, "Свойство высоты поля не изменилось");
        }

        [Test(Description = "Тестирование изменения длины цепочки для выигрыша")]
        public void LengthToWinChangeTest()
        {
            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            uint oldLength = map.LengthToWin;
            uint newLength = oldLength + 2;

            map.LengthToWin = newLength;

            Assert.AreEqual(newLength, map.LengthToWin, "Свойство длины цепочки для выигрыша не изменилось");
        }

        [Test(Description = "Тестирование случая, когда изменение длины цепочки для выигрыша приводит к мгновенному завершению игры")]
        public void LengthToWinInstantWinTest()
        {
            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            map.LengthToWin = 4;
            testObject.Map = new Mark[,] { { Mark.Cross, Mark.Cross, Mark.Cross } }; //для выигрыша недостаточно еще 1 отметки

            bool winCalled = false;
            map.WinEvent += mark => winCalled = true;
            
            map.LengthToWin = 2; //выигрыш
            Task.Run(async () =>
            {
                await Task.Delay(500); //задержка для срабатывания события

                Assert.AreEqual(true, winCalled, "Ожидался выигрыш");
            });
        }

        [Test(Description = "Тестирование изменения отметки следующего хода")]
        public void NextMarkChangeTest()
        {
            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            Mark current = map.NextMark;
            Mark next = current == Mark.Cross ? Mark.Naught : Mark.Cross;

            map.NextMark = next;

            Assert.AreEqual(next, map.NextMark, "Свойство следующей отметки не изменилось");
        }

        [Test(Description = "Тестирование очистки поля")]
        public void ClearTest()
        {
            const int size = 3;
            const int point = size - 1;

            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            map.MapWidth = map.MapHeight = size;
            testObject.Map[point, point] = Mark.Cross;

            map.Clear();
            
            Assert.AreEqual(size, map.MapWidth, "Ширина поля изменилась");
            Assert.AreEqual(size, map.MapHeight, "Высота поля изменилась");
            Assert.AreEqual(default(Mark), testObject.Map[point, point], "Поле не очищено");
        }

        [Test(Description = "Тестирование нажатия на поле")]
        public void MapClickedTest()
        {
            const int size = 3;
            const int point = size - 1;
            const Mark mark = Mark.Cross;

            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            map.LengthToWin = size;
            testObject.Map = new Mark[size, size];
            map.NextMark = mark;

            map.MapClicked(point, point);

            Assert.AreEqual(mark, testObject.Map[point,point], "Отметка не была поставлена");
        }

        [Test(Description = "Тестирование обнаружения выигрыша (горизотальная серия отметок)")]
        public void WinHorizontalTest()
        {
            const Mark winner = Mark.Cross;
            WinTestInternal(new Mark[,] { { winner, winner, Mark.None } }, winner, 0, 2);
        }

        [Test(Description = "Тестирование обнаружения выигрыша (вертикальная серия отметок)")]
        public void WinVerticalTest()
        {
            const Mark winner = Mark.Cross;
            WinTestInternal(new Mark[,] { { winner }, { winner }, { Mark.None } }, winner, 2, 0);
        }

        [Test(Description = "Тестирование обнаружения выигрыша (диагональная серия отметок с горизонтальным сдвигом)")]
        public void WinDiagonalWithHorizontalShiftTest()
        {
            const Mark winner = Mark.Cross;
            Mark[,] map = new Mark[,]
            {
                { Mark.None, Mark.Cross, Mark.None , Mark.None },
                { Mark.None, Mark.None , Mark.Cross, Mark.None },
                { Mark.None, Mark.None , Mark.None , Mark.None },
            };

            WinTestInternal(map, winner, 2, 3);
        }

        [Test(Description = "Тестирование обнаружения выигрыша (диагональная серия отметок с вертикальным сдвигом)")]
        public void WinDiagonalWithVerticalShiftTest()
        {
            const Mark winner = Mark.Cross;
            Mark[,] map = new Mark[,]
            {
                { Mark.None , Mark.None , Mark.None },
                { Mark.Cross, Mark.None , Mark.None },
                { Mark.None , Mark.Cross, Mark.None },
                { Mark.None , Mark.None , Mark.None },
            };

            WinTestInternal(map, winner, 3, 2);
        }

        [Test(Description = "Тестирование обнаружения выигрыша (диагональная серия отметок с горизонтальным сдвигом справа-налево)")]
        public void WinInvertedDiagonalWithHorizontalShiftTest()
        {
            const Mark winner = Mark.Cross;
            Mark[,] map = new Mark[,]
            {
                { Mark.None, Mark.None , Mark.None , Mark.Cross },
                { Mark.None, Mark.None , Mark.Cross, Mark.None },
                { Mark.None, Mark.None , Mark.None , Mark.None },
            };

            WinTestInternal(map, winner, 2, 1);
        }

        [Test(Description = "Тестирование обнаружения выигрыша (диагональная серия отметок с вертикальным сдвигом снизу-вверх)")]
        public void WinInvertedDiagonalWithVerticalShiftTest()
        {
            const Mark winner = Mark.Cross;
            Mark[,] map = new Mark[,]
            {
                { Mark.None , Mark.None , Mark.None },
                { Mark.None , Mark.None , Mark.None },
                { Mark.None , Mark.Cross, Mark.None },
                { Mark.Cross, Mark.None , Mark.None },
            };

            WinTestInternal(map, winner, 1, 2);
        }

        private void WinTestInternal(Mark[,] gameMap, Mark expectedWinner, int clickRow, int clickColumn)
        {
            const int lengthToWin = 3;

            GameMapTestObject testObject;
            GameMapViewModel map = Init(out testObject);
            map.LengthToWin = lengthToWin;
            testObject.Map = gameMap;
            map.NextMark = expectedWinner;

            bool winCalled = false;
            Mark winMark = Mark.None;
            map.WinEvent += mark =>
            {
                winCalled = true;
                winMark = mark;
            };

            map.MapClicked(clickRow, clickColumn);

            if (!winCalled)
            {
                Assert.Fail("Выигрыш не был засчитан");
                return;
            }

            Assert.AreNotEqual(winMark, Mark.None, "Выигрыш засчитан за серию пустых клеток");
            Assert.AreEqual(winMark, expectedWinner, "Событие выигрыша не вызвано или вызвано с неверным аргументом");
        }
    }
}
