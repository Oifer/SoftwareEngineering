﻿using System;
using System.Windows.Controls;
using SoftwareEngineering.Models;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Controls.GameMap
{
    /// <summary>
    /// Тип, реализующий логику поля для игры
    /// </summary>
    public class GameMapViewModel : IGameMap
    {
        public GameMapViewModel(Action<Mark[,]> onRedraw)
        {
            _onRedraw = onRedraw;
        }
        
        /// <inheritdoc />
        public uint MapWidth
        {
            get => Convert.ToUInt32(_map.GetLength(1)); 
            set => Resize(value, MapHeight);
        }

        /// <inheritdoc />
        public uint MapHeight
        {
            get => Convert.ToUInt32(_map.GetLength(0)); 
            set => Resize(MapWidth, value);
        }

        private void Resize(uint width, uint height) => _onRedraw(_map = new Mark[height, width]);

        private uint _lengthToWin;

        /// <inheritdoc />
        public uint LengthToWin
        {
            get => _lengthToWin;
            set
            {
                _lengthToWin = value;
                CheckWin();
            }
        }

        /// <inheritdoc />
        public Mark NextMark { get; set; }

        /// <inheritdoc />
        public event Action<Mark> WinEvent;

        /// <inheritdoc />
        public event Action NoEmptyCellsEvent;

        /// <inheritdoc />
        public event Action<Mark> NextMarkChanged;

        /// <inheritdoc />
        public void Clear() => Resize(MapWidth, MapHeight);

        public Mark MapClicked(int row, int column)
        {
            Mark currentMark = _map[row, column] = NextMark;
            NextMarkChanged?.Invoke(NextMark = GetNextMark(NextMark));

            if (!CheckWin())
                CheckFull();

            return currentMark;
        }

        private Mark GetNextMark(Mark currentMark)
        {
            return currentMark.GetValueByMark(
                Mark.Cross,
                Mark.Cross,
                Mark.Naught);
        }

        /// <summary> Проверка поля на наличие серий отметок достаточно длинных для выигрыша </summary>
        private bool CheckWin()
        {
            return CheckHorizontal(_map, LengthToWin) != default ||
                   CheckVertical(_map, LengthToWin) != default ||
                   CheckDiagonal(_map, LengthToWin) != default;
        }

        /// <summary> Проверка горизонтальных серий </summary>
        private Mark CheckHorizontal(Mark[,] map, uint lengthToWin)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                Counter<Mark> state = new Counter<Mark>(map[i, 0], 1);

                for (int j = 1; j < width; j++)
                {
                    Mark item = map[i, j];
                    if (CheckItem(state, item, lengthToWin))
                        return item;
                }
            }

            return Mark.None;
        }

        /// <summary> Проверка вертикальных серий </summary>
        private Mark CheckVertical(Mark[,] map, uint lengthToWin)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            for (int j = 0; j < width; j++)
            {
                Counter<Mark> state = new Counter<Mark>(map[0, j], 1);
                    
                for (int i = 1; i < height; i++)
                {
                    Mark item = map[i, j];
                    if (CheckItem(state, item, lengthToWin))
                        return item;
                }
            }

            return Mark.None;
        }

        /// <summary> Проверка диагональных серий </summary>
        private Mark CheckDiagonal(Mark[,] map, uint lengthToWin)
        {
            Mark horizontalResult = CheckDiagonalWithHorizontalShift(map, lengthToWin);
            if (horizontalResult != default)
                return horizontalResult;

            Mark verticalResult = CheckDiagonalWithVerticalShift(map, lengthToWin);
            if (verticalResult != default)
                return verticalResult;

            return Mark.None;
        }

        /// <summary> Проверка диагональных серий с сдвигом по горизонтали от начала координат </summary>
        private Mark CheckDiagonalWithHorizontalShift(Mark[,] map, uint lengthToWin)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            for (int horizontalShift = 0; horizontalShift < width; horizontalShift++) //сдвиг от верхнего левого края по горизонтали
            {
                Counter<Mark> state = new Counter<Mark>(map[0, horizontalShift], 1);

                for (int i = 1; (i + horizontalShift) < width && i < height; i++) //проход по диагонали до упора в края поля
                {
                    Mark item = map[i, i + horizontalShift];
                    if (CheckItem(state, item, lengthToWin))
                        return item;
                }
            }

            return Mark.None;
        }

        /// <summary> Проверка диагональных серий с сдвигом по вертикали от начала координат </summary>
        private Mark CheckDiagonalWithVerticalShift(Mark[,] map, uint lengthToWin)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            for (int verticalShift = 0; verticalShift < height; verticalShift++)
            {
                Counter<Mark> state = new Counter<Mark>(map[verticalShift, 0], 1);

                for (int i = 1; i < width && (i + verticalShift) < height; i++)
                {
                    Mark item = map[verticalShift + i, i];
                    if (CheckItem(state, item, lengthToWin))
                        return item;
                }
            }

            return Mark.None;
        }

        /// <summary> Проверяет текущий элемент и вызывает событие выигрыша, если цепочка достаточно длинна </summary>
        private bool CheckItem(Counter<Mark> state, Mark item, uint lengthToWin)
        {
            if (!state.Check(item)) 
                return false;
            
            if (state.CurrentItem == Mark.None || state.Count < lengthToWin)
                return false;

            WinEvent?.Invoke(item);
            return true;
        }

        /// <summary> Проверяет остались ли на поле свободные клетки </summary>
        private void CheckFull()
        {
            int heigth = _map.GetLength(0);
            int width = _map.GetLength(1);

            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (_map[i, j] == Mark.None)
                        return;
                }
            }

            //если все поля заполнены
            NoEmptyCellsEvent?.Invoke();
        }


        protected Mark[,] _map = new Mark[,] { };

        private readonly Action<Mark[,]> _onRedraw;
    }
}
