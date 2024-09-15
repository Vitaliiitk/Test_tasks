using Microsoft.AspNetCore.Mvc;
using test_task_bitsOrchestra.Models;

namespace test_task_bitsOrchestra.Repository.Interfaces
{
    public interface IPersonRepository
    {
        public Task<string> UploadCsv(IFormFile file);

        public Task<bool> UpdatePerson([FromBody] Person updatedPerson);

        public Task<bool> DeletePerson(int id);

        public Task<List<Person>> ViewPersons();
    }
}
