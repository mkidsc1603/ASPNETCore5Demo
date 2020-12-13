using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo.Models.Custom
{
    public class IN_LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class OUT_LoginModel 
    {
        public string Token { get; set; }
    }

}
