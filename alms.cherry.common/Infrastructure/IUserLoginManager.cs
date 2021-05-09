using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using alms.cherry.common.Models;

namespace alms.cherry.common.Infrastructure
{
    public interface IUserLoginManager
    {
        Task TryToLoginByGivenCredentials(string username, SecureString passPhrase);

        Task<IEnumerable<UserLoginModel>> GetLoggedInUsers();
        Task<DateTime> LogoutUser();

        Task ExitFromApplication();
    }
}