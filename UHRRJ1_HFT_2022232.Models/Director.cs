using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UHRRJ1_HFT_2022232.Models
{
    public class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DirectorId { get; set; }

        [Required]
        [StringLength(240)]
        public string DirectorName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }

        public Director()
        {
            Movies = new HashSet<Movie>();
        }

        public Director(string line)
        {
            string[] split = line.Split('#');
            DirectorId = int.Parse(split[0]);
            DirectorName = split[1];
            Movies = new HashSet<Movie>();
        }

        public override bool Equals(object obj)
        {
            Director other = obj as Director;
            return DirectorId == other.DirectorId 
                && DirectorName == other.DirectorName
                && Movies.Equals(other.Movies);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(DirectorId,DirectorName,Movies);
        }
    }

}
