using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Api.Models;

namespace Stock.Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}