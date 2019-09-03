using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreatePayPalSubscriptionCommandTests
    {
        //TODO Note: RED, GREEN, REFACTOR

        #region Private Attributes

        #endregion

        #region Constructors

        #endregion

        #region Should Return Error Tests
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreatePayPalSubscriptionCommand();
            command.FirstName = "";
            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }
        #endregion

        #region Should Return Success Tests

        #endregion
    }
}
