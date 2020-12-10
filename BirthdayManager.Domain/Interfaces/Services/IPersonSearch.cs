using System;
using System.Collections.Generic;
using BirthdayManager.Domain.Entities;

namespace BirthdayManager.Domain.Interfaces.Services
{
    public interface IPersonSearch
    {
        List<Person> SearchByName(List<Person> persons, string query);
        List<Person> SearchByDob(List<Person> persons, DateTime query);
    }
}
