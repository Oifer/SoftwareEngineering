using SoftwareEngineering.Models.Enums;
using SoftwareEngineering.View.Controls.GameMap;

namespace SoftwareEngineering.Test.TestObjects.View.Controls
{
    /// <summary>
    /// тестовый объект для <seealso cref="GameMapViewModel"/>
    /// </summary>
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
