using BirthdayManager.Domain.Entities;

namespace BirthdayManager.Domain.Interfaces.Services
{
    public interface IPersonServiceDobInfo
    {
        int DaysForNextBirthday(int month, int day);
        void DisplayBirthdaysOfTheDay(Person person);
    }
}
