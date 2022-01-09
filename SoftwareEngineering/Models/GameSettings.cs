using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.Models
{
    /// <summary> Тип, описывающий настройки для игры </summary>
    public class GameSettings
    {
        public GameSettings(uint mapWidth, uint mapHeight, uint lengthToWin, Mark firstMark)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            LengthToWin = lengthToWin;
            FirstMark = firstMark;
        }

        public uint MapWidth { get; }

        public uint MapHeight { get; }

        public uint LengthToWin { get; }

        public Mark FirstMark { get; }
    }
}
