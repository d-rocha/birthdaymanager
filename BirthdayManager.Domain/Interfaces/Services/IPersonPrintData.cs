using System;
using System.Collections.Generic;
using BirthdayManager.Domain.Entities;

namespace BirthdayManager.Domain.Interfaces.Services
{
    public interface IPersonPrintData
    {
        string PrintFullName(Person person);
        string PrintFullData(Person person);
    }
}
