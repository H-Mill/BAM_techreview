using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Queries;
using System.Net;

namespace StargateAPI.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var result = await _mediator.Send(new GetPeople());
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new CreateExceptionLog(ex));
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonByName(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetPersonById()
                {
                    Id = id
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new CreateExceptionLog(ex));
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePerson createPerson)
        {
            try
            {
                var result = await _mediator.Send(createPerson);

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new CreateExceptionLog(ex));
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }

        }
    }
}