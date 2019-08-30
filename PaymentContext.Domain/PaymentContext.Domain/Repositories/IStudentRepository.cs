using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        #region Methods

        #region Sync Methods
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
        #endregion

        #endregion
    }
}
