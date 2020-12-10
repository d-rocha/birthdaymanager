using BirthdayManager.Domain.Entities;

namespace BirthdayManager.Domain.Interfaces.Repositories
{
    public interface IPersonCreateOnFile
    {
        void CreateOnFile(Person person);
    }
}
