using BirthdayManager.Domain.Entities;

namespace BirthdayManager.Domain.Interfaces.Repositories
{
    public interface IPersonUpdateOnFile
    {
        void UpdateOnFile(Person person);
    }
}
