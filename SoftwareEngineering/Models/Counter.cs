namespace SoftwareEngineering.Models
{
    /// <summary>
    /// Тип, описывающий счетчик элементов
    /// </summary>
    public class Counter<T>
    {
        public Counter(T currentItem, uint count)
        {
            CurrentItem = currentItem;
            Count = count;
        }

        /// <summary> Текущее значение </summary>
        public T CurrentItem { get; private set; }

        /// <summary> Значение счетчика </summary>
        public uint Count { get; private set; }

        /// <summary>
        /// Сравнивает значение с текущим и поднимает счетчик, если необходимо
        /// </summary>
        /// <returns>Был ли элемент таким же как <see cref="CurrentItem"/> или нет</returns>
        public bool Check(T item)
        {
            if (item.Equals(CurrentItem))
            {
                Count++;
                return true;
            }

            CurrentItem = item;
            Count = 1;
            return false;
        }
    }
}
