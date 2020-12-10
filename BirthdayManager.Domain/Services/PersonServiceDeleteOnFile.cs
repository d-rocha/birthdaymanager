using System;
using System.IO;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Repositories;

namespace BirthdayManager.Domain.Services
{
    public class PersonServiceDeleteOnFile
    {
        private readonly IPersonDeleteOnFile _personDeleteOnFile;

        public PersonServiceDeleteOnFile(IPersonDeleteOnFile personDeleteOnFile)
        {
            _personDeleteOnFile = personDeleteOnFile;
        }

        /// <summary>
        /// Delete method, implements the CRUD event of the repository PersonRepositoryFile
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOnFile(Guid id)
        {
            try
            {
                _personDeleteOnFile.DeleteOnFile(id);
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred! The Person not deleted!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
