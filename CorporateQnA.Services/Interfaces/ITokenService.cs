﻿using CorporateQnA.Services.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
        public int GetLoggedInUserId();
    }
}
