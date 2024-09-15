using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using test_task_bitsOrchestra.Data;
using test_task_bitsOrchestra.Models;
using test_task_bitsOrchestra.Repository.Interfaces;

namespace test_task_bitsOrchestra.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> UploadCsv(IFormFile file)
        {

            string isFileUploaded = ""; // some message will be displayed in WEB

            if (file != null && file.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload");

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                    {
                        do
                        {
                            bool isHeaderSkipped = false;

                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                Person person = new Person();

                                person.Name = reader.GetValue(0)?.ToString();

                                if (DateTime.TryParse(reader.GetValue(1)?.ToString(), out DateTime dateOfBirth))
                                    person.DateOfBirth = dateOfBirth;
                                else
                                    Console.WriteLine("Invalid Date format");

                                if (bool.TryParse(reader.GetValue(2)?.ToString(), out bool married))
                                    person.Married = married;

                                else
                                    Console.WriteLine("Invalid Boolean format");

                                person.Phone = reader.GetValue(3)?.ToString();

                                if (decimal.TryParse(reader.GetValue(4)?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal salary))
                                    person.Salary = salary;
                                else
                                    Console.WriteLine("Invalid Decimal format");

                                _context.Add(person);
                                await _context.SaveChangesAsync();
                            }
                        } while (reader.NextResult());

                        isFileUploaded = "Success";
                    }
                }
            }
            else
                isFileUploaded = "Empty";


            return isFileUploaded;
        }

        public async Task<List<Person>> ViewPersons()
        {
            var personList = await _context.Persons.ToListAsync();
            return personList;
        }

        public async Task<bool> UpdatePerson([FromBody] Person updatedPerson)
        {
            var person = await _context.Persons.FindAsync(updatedPerson.Id);
            if (person == null) return false;

            person.Name = updatedPerson.Name;
            person.DateOfBirth = updatedPerson.DateOfBirth;
            person.Married = updatedPerson.Married;
            person.Phone = updatedPerson.Phone;
            person.Salary = updatedPerson.Salary;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
