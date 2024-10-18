using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetAstronautDutiesByName : IRequest<GetAstronautDutiesByNameResult>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetAstronautDutiesByNameHandler : IRequestHandler<GetAstronautDutiesByName, GetAstronautDutiesByNameResult>
    {
        private readonly StargateContext _context;

        public GetAstronautDutiesByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetAstronautDutiesByNameResult> Handle(GetAstronautDutiesByName request, CancellationToken cancellationToken)
        {
            var person = await _context.People
                .Where(person => person.Name == request.Name)
                .Include(p => p.AstronautDetail)
                .Include(p => p.AstronautDuties)
                .Select(p => new AstronautDTO(p))
                .FirstAsync();
            var result = new GetAstronautDutiesByNameResult(person);
            if (result.Person == null)
                throw new ArgumentNullException(nameof(result.Person));
            return result;
        }
    }

    public class GetAstronautDutiesByNameResult : BaseResponse
    {
        public GetAstronautDutiesByNameResult(AstronautDTO person)
        {
            Person = person;
        }

        public AstronautDTO Person { get; set; }
    }
}
