using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NETCore.Model
{
    [Table("tb_tr_accounts")]
    public class Account
    {
        //[Key]
        //[ForeignKey("NIK")]
        [Key]
        public string NIK { get; set; }

        [Required]
        [StringLength(100,MinimumLength =8)]

        
        public string Password { get; set; }

        [JsonIgnore]
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }

        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
        public Account(string nIK, string password)
        {
            NIK = nIK;
            Password = password;
        }
    }
}
