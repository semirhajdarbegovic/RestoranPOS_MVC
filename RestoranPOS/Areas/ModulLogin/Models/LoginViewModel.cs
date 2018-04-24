using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranPOS.Areas.ModulLogin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unos je obavezan za Username polje!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Unos je obavezan za Password polje!")]
        public string Password { get; set; }

    }
}