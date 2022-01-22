using NUnit.Framework;

using SoftwareEngineering.Models;

namespace SoftwareEngineering.Test.Tests.Models
{
    [TestFixture]
    public class CounterTests
    {
        private Counter<T> Init<T>(T item, uint count)
        {
            return new Counter<T>(item, count);
        }

        [Test(Description = "Тестирование корректности срабатывания встроенной проверки")]
        public void CheckTest()
        {
            const int initialItem = 2;
            const uint initialCount = 0;

            Counter<int> state = Init(initialItem, initialCount);

            Assert.IsTrue(state.Check(initialItem));
            Assert.AreEqual(initialCount + 1, state.Count);

            Assert.IsFalse(state.Check(initialItem - 1));
            Assert.AreEqual(1, state.Count);
            Assert.AreEqual(initialItem - 1, state.CurrentItem);
        }
    }
}
