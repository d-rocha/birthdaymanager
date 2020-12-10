using System.Collections.Generic;
using BirthdayManager.Domain.Entities;

namespace BirthdayManager.Domain.Interfaces.Repositories
{
    public interface IPersonReadOnFile
    {
        List<Person> ReadOnFile();
    }
}
