using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez!")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Lütfen parolanızı giriniz!")]
        public string Password { get; set; }

    }
}
