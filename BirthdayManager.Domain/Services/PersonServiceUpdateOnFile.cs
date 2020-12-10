using System;
using System.Collections.Generic;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Repositories;

namespace BirthdayManager.Domain.Services
{
    public class PersonServiceUpdateOnFile : IPersonUpdateOnFile
    {
        private readonly IPersonUpdateOnFile _personUpdateOnFile;

        public PersonServiceUpdateOnFile(IPersonUpdateOnFile personUpdateOnFile)
        {
            _personUpdateOnFile = personUpdateOnFile;
        }

        public void UpdateOnFile(Person person)
        {
            Person personUpdate = new Person();
            personUpdate.Id = person.Id;
            personUpdate.Name = person.Name;
            personUpdate.LastName = person.LastName;
            personUpdate.Dob = person.Dob;
            personUpdate.Email = person.Email;
            personUpdate.PhoneNumber = person.PhoneNumber;


            if (personUpdate.Id == person.Id && person.Name == "" && person.LastName == "" && person.Dob == null)
                return;

            _personUpdateOnFile.UpdateOnFile(personUpdate);
        }
    }
}
