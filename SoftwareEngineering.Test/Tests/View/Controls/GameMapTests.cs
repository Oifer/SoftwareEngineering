﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SoftwareEngineering.Models.Enums;
using SoftwareEngineering.Test.TestObjects.View.Controls;
using SoftwareEngineering.View.Controls.GameMap;

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
            Init(out GameMapTestObject testObject);
            Assert.Pass();
        }

        [Test(Description = "Тестирование изменения ширины поля")]
        public void WidthChangeTest()
        {
            GameMapViewModel map = Init(out GameMapTestObject testObject);
            int oldWidth = map.MapWidth;
            int newWidth = oldWidth + 2;

            map.MapWidth = newWidth;

            Assert.AreEqual(newWidth, testObject.Map.GetLength(1), "Фактическая ширина не изменилась");
            Assert.AreEqual(newWidth, map.MapWidth, "Свойство ширины поля не изменилось");
        }

        [Test(Description = "Тестирование изменения высоты поля")]
        public void HeightChangeTest()
        {
            GameMapViewModel map = Init(out GameMapTestObject testObject);
            int oldHeight = map.MapHeight;
            int newHeight = oldHeight + 2;

            map.MapHeight = newHeight;

            Assert.AreEqual(newHeight, testObject.Map.GetLength(0), "Фактическая высота поля не изменилась");
            Assert.AreEqual(newHeight, map.MapHeight, "Свойство высоты поля не изменилось");
        }

        [Test(Description = "Тестирование изменения длины цепочки для выигрыша")]
        public void LengthToWinChangeTest()
        {
            GameMapViewModel map = Init(out GameMapTestObject testObject);
            int oldLength = map.LengthToWin;
            int newLength = oldLength + 2;

            map.LengthToWin = newLength;

            Assert.AreEqual(newLength, map.LengthToWin, "Свойство длины цепочки для выигрыша не изменилось");
        }

        [Test(Description = "Тестирование случая, когда изменение длины цепочки для выигрыша приводит к мгновенному завершению игры")]
        public void LengthToWinInstantWinTest()
        {
            GameMapViewModel map = Init(out GameMapTestObject testObject);
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
            GameMapViewModel map = Init(out GameMapTestObject testObject);
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

            GameMapViewModel map = Init(out GameMapTestObject testObject);
            map.MapWidth = map.MapHeight = size;
            testObject.Map[point, point] = Mark.Cross;

            map.Clear();
            
            Assert.AreEqual(size, map.MapWidth, "Ширина поля изменилась");
            Assert.AreEqual(size, map.MapHeight, "Высота поля изменилась");
            Assert.AreEqual(default(Mark), testObject.Map[point, point], "Поле не очищено");
        }
    }
}