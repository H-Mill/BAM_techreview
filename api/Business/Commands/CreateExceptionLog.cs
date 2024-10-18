using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Controllers;
using System.ComponentModel.DataAnnotations;

namespace StargateAPI.Business.Commands
{
    public class CreateExceptionLog : IRequest<CreateExceptionLogResult>
    {
        public CreateExceptionLog() { }
        public CreateExceptionLog(Exception exception)
        {
            this.exception = exception;
        }
        public Exception exception { get; set; }
    }

    public class CreateExceptionLogPreProcessor : IRequestPreProcessor<CreateExceptionLog>
    {
        private readonly StargateContext _context;
        public CreateExceptionLogPreProcessor(StargateContext context)
        {
            _context = context;
        }
        public Task Process(CreateExceptionLog request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException("Request cannot be null.");
            if (request.exception == null) throw new ArgumentNullException("Request cannot be null.");
            if (string.IsNullOrWhiteSpace(request.exception.Message)) throw new ArgumentNullException("Message cannot be null or empty.");
            return Task.CompletedTask;
        }
    }

    public class CreateExceptionLogHandler : IRequestHandler<CreateExceptionLog, CreateExceptionLogResult>
    {
        private readonly StargateContext _context;

        public CreateExceptionLogHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreateExceptionLogResult> Handle(CreateExceptionLog request, CancellationToken cancellationToken)
        {
            try
            {
                var exceptionLog = new ExceptionLogs(request);
                await _context.ExceptionLogs.AddAsync(exceptionLog);
                await _context.SaveChangesAsync();

                return new CreateExceptionLogResult
                {
                    Id = exceptionLog.Id,
                };
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error saving to the database: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

    public class CreateExceptionLogResult : BaseResponse
    {
        public int Id { get; set; }
    }
}
