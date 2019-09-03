using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        #region Constructors
        public CommandResult()
        {

        }
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        #endregion

        #region Attributes
        public bool Success { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
