using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Domain.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound(string userName)
            : base($"User {userName} not found")
        {

        }
    }
}
