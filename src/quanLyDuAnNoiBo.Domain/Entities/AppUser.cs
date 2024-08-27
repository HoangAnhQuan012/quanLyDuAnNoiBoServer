using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace quanLyDuAnNoiBo.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(QuanLyDuAnNoiBoConst.MaxNameLength)]
        public string FirstName { get; set; }

        [Required]
        public string FullName { get; set; }

        public Guid ChucDanhId { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {Surname}";
        }

        public void SetUserName(string userName)
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
        }

        public string GetEmail(string email)
        {
            return email;
        }
    }
}
