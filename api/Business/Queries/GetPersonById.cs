using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetPersonById : IRequest<GetPersonByNameResult>
    {
        public required int Id { get; set; } = -1;
    }

    public class GetPersonByNameHandler : IRequestHandler<GetPersonById, GetPersonByNameResult>
    {
        private readonly StargateContext _context;
        public GetPersonByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetPersonByNameResult> Handle(GetPersonById request, CancellationToken cancellationToken)
        {
            var person = await _context.People
                .Where(p => p.Id == request.Id)
                .Include(p => p.AstronautDetail)
                .Include(p => p.AstronautDuties)
                .Select(p => new AstronautDTO(p))
                .FirstOrDefaultAsync();

            if (person is null) throw new ArgumentNullException($"This person does not exist.");

            var result = new GetPersonByNameResult
            {
                Person = person
            };

            return result;
        }
    }

    public class GetPersonByNameResult : BaseResponse
    {
        public AstronautDTO? Person { get; set; }
    }
}
