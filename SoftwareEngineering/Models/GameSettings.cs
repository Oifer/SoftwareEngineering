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
        public GameSettings()
        { }

        public uint MapWidth { get; set; } = 10;

        public uint MapHeight { get; set; } = 10;

        public uint LengthToWin { get; set; } = 5;

        public Mark FirstMark { get; set; } = Mark.Cross;
    }
}
