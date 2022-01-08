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

    public static class MarkExtensions
    {
        public static T GetValueByMark<T>(this Mark mark, T onNone, T onNaught, T onCross)
        {
            switch (mark)
            {
                case Mark.None: return onNone;
                case Mark.Naught: return onNaught;
                case Mark.Cross: return onCross;

                default: throw new NotImplementedException();
            }
        }
    }
}
