using Appeals.Application.Appeals.Commands.CreateAppeal;
using Appeals.Application.Appeals.Commands.DeleteAppeal;
using Appeals.Application.Appeals.Commands.UpdateAppeal;
using Appeals.Application.Appeals.Queries.GetAppeal;
using Appeals.Application.Appeals.Queries.GetAppealList;
using Appeals.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Appeals.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AppealController : BaseController
    {
        private readonly IMapper _mapper;

        public AppealController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AppealListVm>> GetAll() 
        {
            var query = new GetAppealListQuery
            {
                UserId = GetUserId()
            };
            var vm = await GetMediator().Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppealVm>> Get(Guid id) 
        {
            var query = new GetAppealQuery
            {
                UserId = GetUserId(),
                Id = id,
            };
            var vm = await GetMediator().Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAppealDto model) 
        {
            var command = _mapper.Map<CreateAppealCommand>(model);
            command.UserId = GetUserId();
            var vm = await GetMediator().Send(command);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAppealDto model) 
        {
            var command = _mapper.Map<UpdateAppealCommand>(model);
            command.UserId = GetUserId();
            await GetMediator().Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var command = new DeleteAppealCommand
            {
                Id = id,
                UserId = GetUserId(),
            };
            await GetMediator().Send(command);
            return NoContent();
        }
    }
}
