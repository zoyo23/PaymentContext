using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Mocks
{
    public class FakeEmailService : IEmailService
    {
        #region Methods

        #region Public Methods
        public void Send(string to, string email, string subject, string body)
        {

        }
        #endregion

        #endregion
    }
}
