using System;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Services;

namespace BirthdayManager.Domain.Services
{
    public class PersonServicePrintData : IPersonPrintData
    {
        /// <summary>
        /// Print Name + Last name
        /// </summary>
        /// <returns>When called, returns printing the first and last name.</returns>
        public string PrintFullName(Person person)
        {
            return $"Name: {person.Name} {person.LastName}";
        }

        /// <summary>
        /// Printed the person's complete data
        /// </summary>
        /// <returns>Returns name, last name, birthday, e-mail and phone number</returns>
        public string PrintFullData(Person person)
        {
            return $"Id: {person.Id}"
                + "\r\n"
                + PrintFullName(person)
                + "\r\n"
                + $"Date of Birth: {person.Dob}"
                + "\r\n"
                + $"E-mail: {person.Email}"
                + "\r\n"
                + $"Phone Number: {person.PhoneNumber}";
        }
    }
}
