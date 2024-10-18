using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetPeople : IRequest<GetPeopleResult>
    {

    }

    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
    {
        public readonly StargateContext _context;
        public GetPeopleHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<GetPeopleResult> Handle(GetPeople _, CancellationToken cancellationToken)
        {
            var people = await _context.People
                .Include(p => p.AstronautDetail)
                .Include(p => p.AstronautDuties)
                .OrderBy(p => p.Name)
                .Select(p => new AstronautDTO(p))
                .ToListAsync();

            var result = new GetPeopleResult
            {
                People = people
            };

            return result;
        }
    }

    public class GetPeopleResult : BaseResponse
    {
        public List<AstronautDTO> People { get; set; } = new List<AstronautDTO> { };

    }
}
