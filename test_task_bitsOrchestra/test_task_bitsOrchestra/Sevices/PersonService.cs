using Microsoft.AspNetCore.Mvc;
using test_task_bitsOrchestra.Models;
using test_task_bitsOrchestra.Repository.Interfaces;
using test_task_bitsOrchestra.Sevices.Interfaces;

namespace test_task_bitsOrchestra.Sevices
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<string> UploadCsv(IFormFile file)
        {
            return await _personRepository.UploadCsv(file);
        }

        public async Task<List<Person>> ViewPersons()
        {
            var personList = await _personRepository.ViewPersons();
            return personList;
        }

        public async Task<bool> UpdatePerson([FromBody] Person updatedPerson)
        {
            return await _personRepository.UpdatePerson(updatedPerson);
        }

        public async Task<bool> DeletePerson(int id)
        {
            return await _personRepository.DeletePerson(id);
        }

    }
}
