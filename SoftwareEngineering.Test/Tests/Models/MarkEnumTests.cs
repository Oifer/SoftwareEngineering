using System;

using NUnit.Framework;

using SoftwareEngineering.Models.Enums;

namespace SoftwareEngineering.Test.Tests.Models
{
    [TestFixture]
    public class MarkEnumTests
    {
        [Test(Description = "Тестирование расширения, выдающего один из аргументов в зависимости от значения экземпляра перечисления")]
        public void GetByValueTest()
        {
            Func<Mark, Mark> testFunc = m => MarkExtensions.GetValueByMark(m, Mark.None, Mark.Naught, Mark.Cross);

            Assert.AreEqual(Mark.None, testFunc(Mark.None));
            Assert.AreEqual(Mark.Naught, testFunc(Mark.Naught));
            Assert.AreEqual(Mark.Cross, testFunc(Mark.Cross));
        }
    }
}
