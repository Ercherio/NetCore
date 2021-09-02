using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NETCore.Model
{
    [Table("tb_tr_educations")]
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public int UniversityId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }

        [JsonIgnore]
        public virtual University University { get; set; }

        public Education(string degree, string gPA)
        {
            Degree = degree;
            GPA = gPA;
        }

        public Education(string degree, string gPA, int universityId) : this(degree, gPA)
        {
            UniversityId = universityId;
        }
    }
}
