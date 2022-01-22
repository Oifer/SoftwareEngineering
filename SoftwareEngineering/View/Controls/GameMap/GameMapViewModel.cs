using System;
using System.Diagnostics;
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
            get { return Convert.ToUInt32(_map.GetLength(1)); }
            set { Resize(value, MapHeight); }
        }

        /// <inheritdoc />
        public uint MapHeight
        {
            get { return Convert.ToUInt32(_map.GetLength(0)); }
            set { Resize(MapWidth, value); }
        }

        private void Resize(uint width, uint height)
        {
            _onRedraw(_map = new Mark[height, width]);
        }

        private uint _lengthToWin;

        /// <inheritdoc />
        public uint LengthToWin
        {
            get { return _lengthToWin; }
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
        public void Clear()
        {
            Resize(MapWidth, MapHeight);
        }

        public Mark MapClicked(int row, int column)
        {
            Mark currentMark = _map[row, column] = NextMark;
            if (NextMarkChanged != null)
                NextMarkChanged(NextMark = GetNextMark(NextMark));

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
            return CheckHorizontal(_map, LengthToWin) != default(Mark) ||
                   CheckVertical(_map, LengthToWin) != default(Mark) ||
                   CheckMainDiagonals(_map, LengthToWin) != default(Mark) || 
                   CheckSecondaryDiagonals(_map, LengthToWin) != default(Mark);
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
        
        /// <summary> Проверка диагональных серий, параллельных главной диагонали поля </summary>
        private Mark CheckMainDiagonals(Mark[,] map, uint lengthToWin)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);
            
            //итерация по сдвигу относительно главной диагонали
            //первое значение - значение, при котором диагональ задевает только левый-нижний элемент
            //последнее значение - значение, при котором диагональ задевает только верхний-правый элемент
            for (int shift = -(height-1); shift < width; shift++)
            {
                int startRow = shift >= 0 ? 0 : -shift; //строка первого рассматриваемого элемента
                int startColumn = shift >= 0 ? shift : 0; //столбец первого рассматриваемого элемента
                
                Counter<Mark> state = new Counter<Mark>(map[startRow, startColumn], 1);
                
                //проверка элементов, лежащих на рассматриваемой диагонали
                for (int i = startRow + 1; (i + shift) < width && i < height; i++)
                {
                    Mark item = map[i, startColumn + (i - startRow)];
                    if (CheckItem(state, item, lengthToWin))
                        return item;
                }
            }
            
            return Mark.None;
        }

        /// <summary> Проверка диагональных серий, параллельных побочной диагонали поля </summary>
        private Mark CheckSecondaryDiagonals(Mark[,] map, uint lengthToWin)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            //итерация по сдвигу относительно от левого-верхнего угла (второстепенная диагональ будет иметь сдвиг shift = width-1)
            //первое значение - значение, при котором диагональ задевает только нижний-правый элемент
            //последнее значение - значение, при котором диагональ задевает только верхний-левый элемент
            for (int shift = (width + height) - 1; shift >= 0; shift--)
            {
                int startRow = shift < width? 0 : shift - width; //строка первого рассматриваемого элемента
                int startColumn = shift < width ? shift : width - 1; //столбец первого рассматриваемого элемента
                
                Counter<Mark> state = new Counter<Mark>(map[startRow, startColumn], 1);

                //проход по элементам, лежащим на диагонали
                for (int i = 1; (startColumn - i) >= 0 && (i + startRow) < height; i++)
                {
                    Mark item = map[startRow + i, startColumn - i];
                    if (CheckItem(state, item, lengthToWin))
                        return item;
                }
            }

            return Mark.None;
        }
        
        /// <summary> Проверяет текущий элемент и вызывает событие выигрыша, если цепочка достаточно длинна </summary>
        /// <returns> Было ли вызвано событие</returns>
        private bool CheckItem(Counter<Mark> state, Mark item, uint lengthToWin)
        {
            if (!state.Check(item)) 
                return false;
            
            if (state.CurrentItem == Mark.None || state.Count < lengthToWin)
                return false;

            if (WinEvent != null)
                WinEvent(item);
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
            if (NoEmptyCellsEvent != null)
                NoEmptyCellsEvent();
        }


        protected Mark[,] _map = new Mark[,] { };

        private readonly Action<Mark[,]> _onRedraw;
    }
}
