using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace secretSanta_Login.Models
{
    public class Username
    {
        public int IdUsername { get; set; }
        public string NombreCompleto { get; set; }
        public string Gift { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}