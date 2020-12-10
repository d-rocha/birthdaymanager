using System;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Interfaces.Services;

namespace BirthdayManager.Domain.Services
{
    public class PersonServiceDobInfo : IPersonServiceDobInfo
    {
        /// <summary>
        /// Calculates how many days are left until the next birthday
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns>Returns the remaining days until the next birthday</returns>
        public int DaysForNextBirthday(int month, int day)
        {
            //Intance the daysCalc variable with today's date value (Year, Month, Day)
            DateTime daysCalc = new DateTime(DateTime.Today.Year, month, day);

            //Checks if the month today is bigger than the month received in the function parameter
            if (DateTime.Today.Month > daysCalc.Month || DateTime.Today.Month == daysCalc.Month && DateTime.Today.Day > daysCalc.Day)
                //If greater, add one year to the daysCalc variable
                daysCalc = new DateTime(DateTime.Today.Year + 1, month, day);

            //Returns an integer value resulting from the operation: (daysCalc - current date)
            return (int)daysCalc.Subtract(DateTime.Today).TotalDays;
        }

        /// <summary>
        ///Load birthdays for the day 
        /// </summary>
        public void DisplayBirthdaysOfTheDay(Person person)
        {
            if (person.Dob.Day == DateTime.Today.Day && person.Dob.Month == DateTime.Today.Month)
            {
                Console.WriteLine($"Name: {person.Name} {person.LastName} - {person.Dob}");
            }
        }
    }
}
