using DAT154Oblig4.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT154Oblig4.Application.Common.Identity
{
    public interface IAuthentication
    {
        public string GenerateJWT(CustomerDto user);
        
    }
}
