using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using test_task_bitsOrchestra.Models;
using test_task_bitsOrchestra.Sevices.Interfaces;

namespace test_task_bitsOrchestra.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IPersonService _personService;

		public HomeController(ILogger<HomeController> logger, IPersonService personService)
		{
			_logger = logger;
			_personService = personService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        public IActionResult UploadCsv()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ViewBag.Message = await _personService.UploadCsv(file);

            return View();
        }

        public async Task<IActionResult> ViewPersons()
		{
			var personList = await _personService.ViewPersons();
			return View(personList);
		}

        [HttpPost]
        public async Task<IActionResult> UpdatePerson([FromBody] Person updatedPerson)
        {
            var updatePerson = await _personService.UpdatePerson(updatedPerson);
            if (updatePerson == false) return NotFound();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var deletePerson = await _personService.DeletePerson(id);
            if (deletePerson == false) return NotFound();

            return Ok();
        }
    }
}