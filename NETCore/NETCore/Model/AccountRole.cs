using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Model
{
    [Table("tb_tr_accountroles")]
    public class AccountRole
    {
        public string AccountId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }

        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }

        public AccountRole(string accountId, int roleId)
        {
            AccountId = accountId;
            RoleId = roleId;
        }
    }
}
