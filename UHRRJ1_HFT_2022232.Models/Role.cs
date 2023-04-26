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
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        public int Priority { get; set; }
        public string RoleName { get; set; }

        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Actor Actor { get; private set; }
        [JsonIgnore]
        public virtual Movie Movie { get; private set; }

        public Role()
        {

        }

        public Role(string line)
        {
            string[] split = line.Split('#');
            RoleId = int.Parse(split[0]);
            MovieId = int.Parse(split[1]);
            ActorId = int.Parse(split[2]);
            Priority = int.Parse(split[3]);
            RoleName = split[4];
        }

        public override bool Equals(object obj)
        {
            Role other = obj as Role;
            return RoleId == other.RoleId
                && MovieId == other.MovieId
                && ActorId == other.ActorId
                && Priority == other.Priority
                && RoleName == other.RoleName
                && Actor.Equals(other.Actor)
                && Movie.Equals(other.Movie); 
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoleId,MovieId,ActorId,Priority,RoleName,Actor,Movie);
        }
    }
}
