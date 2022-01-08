using SoftwareEngineering.Models.Enums;
using SoftwareEngineering.View.Controls.GameMap;

namespace SoftwareEngineering.Test.TestObjects.View.Controls
{
    public class GameMapTestObject : GameMapViewModel
    {
        public GameMapTestObject() : base(map => { })
        { }
        
        public Mark[,] Map
        {
            get => base._map;
            set => base._map = value;
        }
    }
}
