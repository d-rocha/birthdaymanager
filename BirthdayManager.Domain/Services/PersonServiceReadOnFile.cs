using System;
using System.IO;
using System.Collections.Generic;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Repositories;

namespace BirthdayManager.Domain.Services
{
    public class PersonServiceReadOnFile : IPersonReadOnFile
    {
        private readonly IPersonReadOnFile _personReadOnFile;

        public PersonServiceReadOnFile(IPersonReadOnFile personReadOnFile)
        {
            _personReadOnFile = personReadOnFile;
        }

        /// <summary>
        /// ReadOn method, implements the CRUD event of the repository PersonRepositoryFile
        /// </summary>
        /// <returns>Return a person's list</returns>
        public List<Person> ReadOnFile()
        {
            string filePath = @"D:\BirthdayManager\persondata.txt"; ;

            try
            {
                FileStream fs;
                if (!File.Exists(filePath))
                {
                    fs = File.Create(filePath);
                    fs.Close();
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("An error occurred!");
                Console.WriteLine(e.Message);
            }

            return _personReadOnFile.ReadOnFile();
        }
    }
}
