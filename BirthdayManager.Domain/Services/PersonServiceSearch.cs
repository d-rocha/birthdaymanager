using System;
using System.Linq;
using System.Collections.Generic;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Services;

namespace BirthdayManager.Domain.Services
{
    public class PersonServiceSearch : IPersonSearch
    {
        public List<Person> SearchByName(List<Person> persons, string query)
        {
            var personslist = persons.Where(
                p => p.Name.Contains(query.ToUpper()));
            
            return personslist.ToList();
        }

        public List<Person> SearchByDob(List<Person> persons, DateTime query)
        {
            var personslist = persons.Where(
                p => p.Dob == query);

            return personslist.ToList();
        }
    }
}
