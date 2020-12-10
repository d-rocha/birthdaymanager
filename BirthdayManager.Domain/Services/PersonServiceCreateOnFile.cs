using System;
using System.IO;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Repositories;

namespace BirthdayManager.Domain.Services
{
    public class PersonServiceCreateOnFile
    {
        private readonly IPersonCreateOnFile _personCreateOnFile;
        
        public PersonServiceCreateOnFile(IPersonCreateOnFile personCreateOnFile)
        {
            _personCreateOnFile = personCreateOnFile;
        }

        /// <summary>
        /// Register method, implements the CRUD event of the repository PersonRepositoryFile
        /// </summary>
        /// <param name="person"></param>
        public void Register(Person person)
        {
            try
            {
                if (person.Name == "" && person.LastName == "" && person.Dob == null)
                    return;

                _personCreateOnFile.CreateOnFile(person);
            }
            catch(IOException e)
            {
                Console.WriteLine("An error occurred! Person not registred!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
