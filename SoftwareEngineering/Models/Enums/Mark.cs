using System;

namespace SoftwareEngineering.Models.Enums
{
    /// <summary> Перечисление отметок, помещаемых в ячейку поля </summary>
    public enum Mark
    {
        /// <summary> Пустое поле </summary>
        None = default(int),
        
        /// <summary> Ноль </summary>
        Naught,
        
        /// <summary> Крест </summary>
        Cross,
    }

    public static class MarkExtensions
    {
        /// <summary>
        /// Получение знания в зависимости от экземпляра перечисления
        /// </summary>
        /// <remarks>В случае внесения изменений в перечисление позволяет переложить поиск использований на транслятор</remarks>
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
