using Appeals.Application.Appeals.Commands.CreateAppeal;
using Appeals.Application.Appeals.Commands.DeleteAppeal;
using Appeals.Application.Appeals.Commands.UpdateAppeal;
using Appeals.Application.Appeals.Queries.GetAppeal;
using Appeals.Application.Appeals.Queries.GetAppealList;
using Appeals.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Appeals.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}[controller]")]
    public class AppealController : BaseController
    {
        private readonly IMapper _mapper;

        public AppealController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of appeals
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /appeal
        /// </remarks>
        /// <returns>Returns AppealListVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If user is unauthorized</responce>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AppealListVm>> GetAll() 
        {
            var query = new GetAppealListQuery
            {
                UserId = GetUserId()
            };
            var vm = await GetMediator().Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the appeal by guid
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /appeal/17CAADFF-DF8F-42E8-9C70-0939DA22567C
        /// </remarks>
        /// <param name="id">Appeal Id (guid)</param>
        /// <returns>Returns AppealVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If user is unauthorized</responce>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Create the appeal
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /appeal
        /// {
        ///     title: "test title",
        ///     description: "test description"
        /// }
        /// </remarks>
        /// <param name="model">CreateAppealDto object</param>
        /// <returns>Returns if (guid)</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If user is unauthorized</responce>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAppealDto model) 
        {
            var command = _mapper.Map<CreateAppealCommand>(model);
            command.UserId = GetUserId();
            var vm = await GetMediator().Send(command);
            return Ok(vm);
        }

        /// <summary>
        /// Update the appeal
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /appeal
        /// {
        ///     title: "update title",
        ///     description: "update description"
        /// }
        /// </remarks>
        /// <param name="model">UpdateAppealDto object</param>
        /// <returns>Returns No Content</returns>
        /// <responce code="204">Success</responce>
        /// <responce code="401">If user is unauthorized</responce>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateAppealDto model) 
        {
            var command = _mapper.Map<UpdateAppealCommand>(model);
            command.UserId = GetUserId();
            await GetMediator().Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete appeal by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /appeal/6E9279D1-111A-435A-BA83-58B3137D483C
        /// </remarks>
        /// <param name="id">Id of the appeal</param>
        /// <returns>Returns NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <responce code="401">If user is unauthorized</responce>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
