using GameCatalogue.Api.Helpers;
using GameCatalogue.Api.Models;
using GameCatalogue.Application.CQRS.Commands;
using GameCatalogue.Application.CQRS.Queries;
using GameCatalogue.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GamesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetById(long id)
        {
            var dto = await _mediator.Send(new GetGameByIdQuery(id));
            if (dto is null)
                return NotFound();

            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GameDto>>> GetAll(
            [FromQuery] string platforms = "all",
            [FromQuery] string prices = "",
            [FromQuery] int page = 1,
            [FromQuery] int size = 4)
        {
            var platformList = platforms.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var priceList = prices.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var result = await _mediator.Send(new GetGamesQuery(page, size,platformList, priceList));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GameDto>> Update([FromForm] UpdateGameFormModel form)
        {
            var (isValid, error, stream, ext) = FileUploadHelper.ProcessImageFile(form.ImageFile);
            if (!isValid)
                return BadRequest(error);

            var command = new UpdateGameCommand(form.Id, form.Name, form.Price, form.LastModified, form.Platforms, stream, ext);
            var dto = await _mediator.Send(command);

            return Ok(dto);
        }
    }
}
