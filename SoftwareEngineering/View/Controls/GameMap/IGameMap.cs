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
        uint MapWidth { get; set; }

        /// <summary> Высота поля </summary>
        uint MapHeight { get; set; }

        /// <summary> Количество одинаковых отметок, которые необходимо поместить в ряд для выигрыша </summary>
        uint LengthToWin { get; set; }

        /// <summary> Отметка, которая будет поставлена при следующем нажатии </summary>
        Mark NextMark { get; set; }

        /// <summary> Событие, происходящее при выигрыше </summary>
        event Action<Mark> WinEvent;

        /// <summary> Событие, происходящее при заполнении всех клеток </summary>
        event Action NoEmptyCellsEvent;

        /// <summary> Событие, происходящее при изменении <seealso cref="NextMark"/> </summary>
        event Action<Mark> NextMarkChanged;

        /// <summary> Очиста поля </summary>
        void Clear();
    }
}
