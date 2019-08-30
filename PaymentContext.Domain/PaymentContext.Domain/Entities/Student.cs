using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        #region Private Attributes
        private IList<Subscription> _subscriptions;
        #endregion

        #region Constructors
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(Name, Document, Email);
        }
        #endregion

        #region Attributes
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }
        #endregion

        #region Methods

        #region Public Methods
        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Studet.Subscriptions", "Você já tem uma assinatura ativa.")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payment", "Esta assinatura não possui pagamentos."));

            //if (hasSubscriptionActive)
            //{
            //    AddNotification("Studet.Subscriptions", "Você já tem uma assinatura ativa.");
            //}
        }
        #endregion

        #endregion
    }
}
