using System;
using System.IO;
using System.Collections.Generic;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Repositories;

namespace BirthdayManager.Infrastructure.Repositories
{
    public class PersonRepositoryFile : IPersonCreateOnFile, IPersonReadOnFile, IPersonUpdateOnFile, IPersonDeleteOnFile
    {
        /// <summary>
        ///  CRUD Method, Create
        /// </summary>
        /// <param name="person"></param>
        public void CreateOnFile(Person person)
        {
            //Checks if a file exists
            HasFile();

            //Assigns the filename variable to the file path, returned by GetFileName
            string fileName = GetFileName();

            try
            {
                var newPerson = $"{person.Id}," +
                                $"{person.Name}," +
                                $"{person.LastName}," +
                                $"{person.Dob}," +
                                $"{person.Email}," +
                                $"{person.PhoneNumber}" +
                                $"\r\n";

                File.AppendAllText(fileName, newPerson);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("A new file has created!");
            }
        }

        /// <summary>
        ///  CRUD Method, Read
        /// </summary>
        /// <returns></returns>
        public List<Person> ReadOnFile()
        {
            //Checks if a file exists
            HasFile();

            //Assigns the filename variable to the file path, returned by GetFileName
            string fileName = GetFileName();

            var Persons = new List<Person>();

            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        var personData = line.Split(',');

                        Person person = new Person();

                        person.Id = Guid.Parse(personData[0]);
                        person.Name = personData[1].ToUpper();
                        person.LastName = personData[2].ToUpper();
                        person.Dob = Convert.ToDateTime(personData[3]);
                        person.Email = personData[4];
                        person.PhoneNumber = personData[5];

                        var removeDuplicate = Persons.Find(p => p.Id == person.Id);

                        Persons.Remove(removeDuplicate);
                        Persons.Add(person);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
            return Persons;
        }

        /// <summary>
        /// CRUD Method, Update
        /// </summary>
        /// <param name="id"></param>
        public void UpdateOnFile(Person person)
        {
            //Checks if a file exists
            HasFile();

            //Assigns the filename variable to the file path, returned by GetFileName
            var fileName = GetFileName();

            //Assigns the tempfile variable to the file temp path, returned by Path.GetTempFileName()
            var tempFile = Path.GetTempFileName();

            //Variable that will store the id of the people that will be edited
            var id = "";

            try
            {                
                using (var sr = new StreamReader(fileName))
                using (var sw = new StreamWriter(tempFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        var personData = line.Split(',');

                        if (person.Id == Guid.Parse(personData[0]))
                            id = personData[0];

                        if (person.Id != Guid.Parse(personData[0]))
                            sw.WriteLine(line);
                    }
                }
                File.Delete(fileName);
                File.Move(tempFile, fileName);

                var personUpdate = $"{person.Id = Guid.Parse(id)}," +
                                   $"{person.Name}," +
                                   $"{person.LastName}," +
                                   $"{person.Dob}," +
                                   $"{person.Email}," +
                                   $"{person.PhoneNumber}" +
                                   $"\r\n";

                File.AppendAllText(fileName, personUpdate);
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred! The chosen person cannot be updated.");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// CRUD Method, Delete
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOnFile(Guid id)
        {
            //Checks if a file exists
            HasFile();

            var fileName = GetFileName();
            var tempFile = Path.GetTempFileName();

            try
            {
                using (var sr = new StreamReader(fileName))
                using (var sw = new StreamWriter(tempFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        var personData = line.Split(',');

                        if (id != Guid.Parse(personData[0]))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                File.Delete(fileName);
                File.Move(tempFile, fileName);
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred! The chosen person cannot be deleted");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Class's private method to get the path and file name
        /// </summary>
        /// <returns>Returns the directory and file path, create a case if none/returns>
        private static string GetFileName()
        {
            string filePath = @"D:\BirthdayManager\persondata.txt";

            return filePath;
        }

        /// <summary>
        /// Class's private method to validate the existence of the file
        /// </summary>
        private void HasFile()
        {
            string fileName = GetFileName();

            FileStream fs;
            if (!File.Exists(fileName))
            {
                fs = File.Create(fileName);
                fs.Close();
            }
        }
    }
}
