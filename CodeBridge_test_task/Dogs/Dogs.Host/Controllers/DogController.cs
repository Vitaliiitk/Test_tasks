using Dogs.Host.Models.Requests;
using Dogs.Host.Models.Responses;
using Dogs.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Dogs.Host.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DogController : ControllerBase
	{
		private readonly ILogger<DogController> _logger;
		private readonly IDogService _dogService;

		public DogController(ILogger<DogController> logger,	IDogService dogService)
		{
			_logger = logger;
			_dogService = dogService;
		}

		[Route("addDog")]
		[HttpPost]
		[ProducesResponseType(typeof(AddDogResponse<int?>), (int)HttpStatusCode.OK)]
		[ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
		public async Task<IActionResult> AddDogAsync(AddDogRequest request)
		{
			try
			{
				var result = await _dogService.AddDogAsync(request.Name, request.Color, request.TailLength, request.Weight);
				return Ok(new AddDogResponse<int?> { Id = result });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (InvalidOperationException ex)
			{
				return Conflict(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError($"An unexpected error occurred in AddDogAsync: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
			}
		}
	}
}
