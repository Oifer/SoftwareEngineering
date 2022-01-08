using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.View.Controls.GameMap
{
    /// <summary> Интерфейс, описывающий поле для игры </summary>
    public interface IGameMap
    {
        /// <summary> Ширина поля </summary>
        int MapWidth { get; set; }

        /// <summary> Высота поля </summary>
        int MapHeight { get; set; }

        /// <summary> Количество одинаковых отметок, которые необходимо поместить в ряд для выигрыша </summary>
        int LengthToWin { get; set; }

        /// <summary> Отметка, которая будет поставлена при следующем нажатии </summary>
        Mark NextMark { get; set; }

        /// <summary> Событие, происходящее при выигрыше </summary>
        event Action<Mark> WinEvent;

        /// <summary> Событие, происходящее при заполнении всех клеток </summary>
        event Action NoEmptyCellsEvent;

        /// <summary> Очиста поля </summary>
        void Clear();
    }
}
