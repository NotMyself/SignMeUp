using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingEntityFramework.Core
{
    public interface IUserRepository
    {
        void Save(User user);
    }
}
