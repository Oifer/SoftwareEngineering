using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering.Models.Enums
{
    /// <summary> Перечисление отметок, помещаемых в ячейку поля </summary>
    public enum Mark
    {
        /// <summary> Пустое поле </summary>
        None = default,
        
        /// <summary> Ноль </summary>
        Naught,
        
        /// <summary> Крест </summary>
        Cross,
    }
}
