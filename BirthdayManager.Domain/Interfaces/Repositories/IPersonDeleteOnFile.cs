using System;

namespace BirthdayManager.Domain.Interfaces.Repositories
{
    public interface IPersonDeleteOnFile
    {
        void DeleteOnFile(Guid id);
    }
}
