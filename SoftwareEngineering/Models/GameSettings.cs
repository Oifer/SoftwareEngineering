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

        public uint MapWidth { get; private set; }

        public uint MapHeight { get; private set; }

        public uint LengthToWin { get; private set; }

        public Mark FirstMark { get; private set; }
    }
}
