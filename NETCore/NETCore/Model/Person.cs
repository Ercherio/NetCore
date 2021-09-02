using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations; //tambahi ini untuk [Key] atau Constrain
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Converters;

namespace NETCore.Model
{
    [Table("tb_m_persons")]
    public class Person
    {
        [Key]   //anotasi Primary Key
        public string NIK { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public int Salary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public enum Gender
        {
            Male,
            Famale
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public string Token { get; set; }


        [JsonIgnore]
        //public string AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
