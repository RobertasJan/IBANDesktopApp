using System;
using IBANDesktopApp.IBAN;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBANDesktopApp.Tests
{
    [TestClass]
    public class IbanValidatorTest
    {
        [TestMethod]
        public void ValidateIban_CorrectIban()
        {
            var ibanValidator = new IbanValidator();
            var correctIban = "LT647044001231465456";

            var result = ibanValidator.ValidateIban(correctIban);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateIban_IncorrectIban()
        {
            var ibanValidator = new IbanValidator();
            var incorrectIban = "CC051245445454552117989";

            var result = ibanValidator.ValidateIban(incorrectIban);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateIban_EmptyIban()
        {
            var ibanValidator = new IbanValidator();
            var emptyIban = "";

            var result = ibanValidator.ValidateIban(emptyIban);

            Assert.IsFalse(result);
        }
    }
}
