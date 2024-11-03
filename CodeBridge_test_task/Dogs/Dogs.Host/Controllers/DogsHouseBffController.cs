using Dogs.Host.Models.Dtos;
using Dogs.Host.Models.Responses;
using Dogs.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Dogs.Host.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DogsHouseBffController : ControllerBase
	{
		private readonly ILogger<DogsHouseBffController> _logger;
		private readonly IDogsHouseService _dogsHouseService;

		public DogsHouseBffController(
			ILogger<DogsHouseBffController> logger,
			IDogsHouseService dogsHouseService)
		{
			_logger = logger;
			_dogsHouseService = dogsHouseService;
		}

		/// <summary>
		/// Retrieves a paginated list of dogs.
		/// </summary>
		/// <param name="pageSize">The number of items to return per page.</param>
		/// <param name="pageIndex">The index of the page to retrieve.</param>
		/// <param name="sortBy">The attribute to sort by (e.g., 'Name', 'Color', 'TailLength', 'Weight').</param>
		/// <param name="ascending">A boolean value indicating the sort order (true for ascending, false for descending).</param>
		/// <returns>A paginated response of dogs.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(PaginatedItemsResponse<DogsDto>), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(StatusCodes.Status429TooManyRequests, Type = typeof(string))]
		public async Task<IActionResult> Dogs([FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] string sortBy = "Name", [FromQuery] bool ascending = true)
		{
			var result = await _dogsHouseService.GetCatalogItemsAsync(pageSize, pageIndex, sortBy, ascending);
			if (result != null && result.Count != 0)
			{
				_logger.LogInformation($"Action \"Dogs\" via dogsHouseService sends httpGet to database, returns data: {JsonConvert.SerializeObject(result)}.");
				return Ok(result);
			}

			return NotFound(result);
		}

		[Route("ping")]
		[HttpGet]
		public async Task<IActionResult> Ping()
		{
			return await Task.FromResult(Ok("Dogshouseservice.Version1.0.1"));
		}
	}
}
