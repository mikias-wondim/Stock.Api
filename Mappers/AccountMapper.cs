using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Api.Dto.Account;
using Stock.Api.Models;

namespace Stock.Api.Mappers
{
    public static class AccountMapper
    {
        public static AppUser ToModelFromRegisterDto(this RegisterRequestDto registerDto)
        {
            return new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
        }
    }
}