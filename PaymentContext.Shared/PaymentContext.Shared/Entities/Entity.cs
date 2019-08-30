using Flunt.Notifications;
using System;

namespace PaymentContext.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        #region Constructors
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Attributes
        public Guid Id { get; private set; }
        #endregion
    }
}
