using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Controllers;
using System.ComponentModel.DataAnnotations;

namespace StargateAPI.Business.Commands
{
    public class CreatePerson : IRequest<CreatePersonResult>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public CreateAstronautDetail? AstronautDetail { get; set; }

        public class CreateAstronautDetail
        {
            public int? Id { get; set; }

            public string? CurrentRank { get; set; }

            public string? CurrentDutyName { get; set; }

            public DateTime? CareerStartDate { get; set; }
        }
    }

    public class CreatePersonPreProcessor : IRequestPreProcessor<CreatePerson>
    {
        private readonly StargateContext _context;
        public CreatePersonPreProcessor(StargateContext context)
        {
            _context = context;
        }
        public Task Process(CreatePerson request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new InvalidOperationException("Invalid request.");
            if (String.IsNullOrWhiteSpace(request.Name))
                throw new InvalidOperationException("Name is required.");
            if (request.AstronautDetail != null && request.AstronautDetail.CareerStartDate == null)
                throw new InvalidOperationException("Career Start Date is required.");

            if (!request.Id.HasValue) return Task.CompletedTask;

            var person = _context.People.AsNoTracking().FirstOrDefault(z => z.Id == request.Id);
            if (person != null) return Task.CompletedTask;

            throw new InvalidOperationException("Invalid update attempt.");
        }
    }

    public class CreatePersonHandler : IRequestHandler<CreatePerson, CreatePersonResult>
    {
        private readonly StargateContext _context;

        public CreatePersonHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreatePersonResult> Handle(CreatePerson request, CancellationToken cancellationToken)
        {
            var person = await _context.People
                .Where(p => p.Id == request.Id)
                .Include(p => p.AstronautDetail)
                .FirstOrDefaultAsync();

            var currentRankUpdate = request.AstronautDetail?.CurrentRank ?? string.Empty;
            var careerStartDateUpdate = request.AstronautDetail?.CareerStartDate ?? DateTime.Now;

            if (person is not null)
            {
                person.Name = request.Name;
                UpdateAstronautDetail(person, request);
            }
            else
            {
                person = new Person()
                {
                    Name = request.Name
                };
                UpdateAstronautDetail(person, request);
                await _context.People.AddAsync(person);
            }

            await _context.SaveChangesAsync();
            return new CreatePersonResult()
            {
                Id = person.Id
            };
        }

        private void UpdateAstronautDetail(Person person, CreatePerson request)
        {
            if (person.AstronautDetail is null)
                person.AstronautDetail = new AstronautDetail();

            var currentRankUpdate = request.AstronautDetail?.CurrentRank ?? string.Empty;
            var careerStartDateUpdate = request.AstronautDetail?.CareerStartDate ?? DateTime.Now;
            if (person.AstronautDetail is not null)
            {
                person.AstronautDetail.CurrentRank = currentRankUpdate;
                person.AstronautDetail.CareerStartDate = careerStartDateUpdate;
            }
        }
    }

    public class CreatePersonResult : BaseResponse
    {
        public int Id { get; set; }
    }
}
