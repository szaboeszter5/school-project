using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHRRJ1_HFT_2022232.Models
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorId { get; set; }

        [Required]
        [StringLength(240)]
        public string ActorName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public Actor()
        {

        }

        public Actor(string line)
        {
            string[] split = line.Split('#');
            ActorId = int.Parse(split[0]);
            ActorName = split[1];
        }
    }

}
