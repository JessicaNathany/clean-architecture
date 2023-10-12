using MediatR;
using Microsoft.Extensions.Logging;

namespace bookfy.application.Abstractions.Behaviors
{
    public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly ILogger<TRequest> _logger;

        public LogginBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                _logger.LogInformation("Executing command {Command}", name);

                var result = await next();

                _logger.LogInformation("Command {Command} processed succesfully", name);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Command {Command} processed failed", name);
                throw;
            }
        }
    }
}
