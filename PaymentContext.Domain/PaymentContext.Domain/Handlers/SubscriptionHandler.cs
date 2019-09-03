using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        #region Attributes
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public SubscriptionHandler(
            IStudentRepository studentRepository,
            IEmailService emailService
            )
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }
        #endregion

        #region Methods

        #region Public Methods
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // FAIL FAST VALIDATION;
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }

            // Verificar se Documento já está cadastrado;
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso.");
            }

            // Verificar se email já está cadastrado;
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este email já está em uso.");
            }

            // Gerar os VO's;
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Domain.Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as entidades;
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.Document, command.PayerDocumentType),
                address,
                email);

            // Relacionamentos;
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações;
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações;
            if (Invalid)
            {
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }

            // Salvar as informações;
            _studentRepository.CreateSubscription(student);

            // Enviar email de boas vindas;
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja Bem Vindo", "Sua assinatura foi criada.");

            // Retornar informações;

            return new CommandResult(true, "Assinatura Realizada com sucesso.");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // FAIL FAST VALIDATION
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }

            // Verificar se Documento já está cadastrado;
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso.");
            }

            // Verificar se email já está cadastrado;
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este email já está em uso.");
            }

            // Gerar os VO's;
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Domain.Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as entidades;
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.Document, command.PayerDocumentType),
                address,
                email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações;
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações;
            if (Invalid)
            {
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }

            // Salvar as informações;
            _studentRepository.CreateSubscription(student);

            // Enviar email de boas vindas;
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Seja Bem Vindo", "Sua assinatura foi criada.");

            // Retornar informações;

            return new CommandResult(true, "Assinatura Realizada com sucesso.");
        }
        #endregion

        #endregion
    }
}
