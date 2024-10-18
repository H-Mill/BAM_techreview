using Dapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Controllers;
using System;
using System.Linq;
using System.Net;

namespace StargateAPI.Business.Commands
{
    public class CreateAstronautDuty : IRequest<CreateAstronautDutyResult>
    {
        public string DutyName { get; set; }
        public DateTime DutyStartDate { get; set; }
        public int PersonId { get; set; }
    }

    public class CreateAstronautDutyPreProcessor : IRequestPreProcessor<CreateAstronautDuty>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyPreProcessor(StargateContext context)
        {
            _context = context;
        }

        public Task Process(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            if (request.DutyName == null)
                throw new InvalidOperationException("Duty Name is required.");
            if (request.DutyStartDate.Date <= DateTime.Now.Date)
                throw new InvalidOperationException("Duty Cannot start today.");

            var latestDuty = _context.AstronautDuties.AsNoTracking().Where(d => d.PersonId == request.PersonId).OrderByDescending(d => d.DutyStartDate).FirstOrDefault();
            if (latestDuty != null && request.DutyStartDate.Date <= latestDuty.DutyStartDate.Date)
                throw new InvalidOperationException($"Duty overlaps with {latestDuty.DutyName}");
            var person = _context.People.AsNoTracking().FirstOrDefault(z => z.Id == request.PersonId);

            if (person is null) throw new BadHttpRequestException("User does not exist.");

            return Task.CompletedTask;
        }
    }

    public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDuty, CreateAstronautDutyResult>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreateAstronautDutyResult> Handle(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            var person = await _context.People
                .Include(p => p.AstronautDetail)
                .Include(p => p.AstronautDuties)
                .Where(p => p.Id == request.PersonId)
                .FirstOrDefaultAsync();


            if (person.AstronautDetail == null)
                person.AstronautDetail = new AstronautDetail();

            HandleInitialRank(person, request);
            HandleRetirement(person, request);
            SetPreviousDutyEndDate(person, request);

            var newAstronautDuty = new AstronautDuty()
            {
                PersonId = person.Id,
                DutyName = request.DutyName,
                DutyStartDate = request.DutyStartDate.Date,
                DutyEndDate = null
            };
            await _context.AstronautDuties.AddAsync(newAstronautDuty);
            await _context.SaveChangesAsync();
            return new CreateAstronautDutyResult()
            {
                Id = newAstronautDuty.Id
            };
        }

        private void HandleInitialRank(Person person, CreateAstronautDuty request)
        {
            if (String.IsNullOrWhiteSpace(person.AstronautDetail.CurrentRank))
            {
                person.AstronautDetail.CurrentRank = "New Hire";
            }
        }

        private void HandleRetirement(Person person, CreateAstronautDuty request)
        {
            const string retired = "RETIRED";
            if (String.Equals(request.DutyName.Trim(), retired, StringComparison.OrdinalIgnoreCase))
            {
                person.AstronautDetail.CareerEndDate = request.DutyStartDate.AddDays(-1).Date;
            }
        }

        private void SetPreviousDutyEndDate(Person person, CreateAstronautDuty request)
        {
            if (person.AstronautDuties != null && person.AstronautDuties.Any())
            {
                person.AstronautDuties
                    .OrderByDescending(d => d.DutyStartDate)
                    .First().DutyEndDate = request.DutyStartDate.AddDays(-1).Date;
            }
        }
    }

    public class CreateAstronautDutyResult : BaseResponse
    {
        public int? Id { get; set; }
    }
}
