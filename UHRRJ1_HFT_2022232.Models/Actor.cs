using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }
        [JsonIgnore]
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

        public override bool Equals(object obj)
        {
            Actor other = obj as Actor;
            return ActorId == other.ActorId
                && ActorName == other.ActorName
                && Roles.Equals(other.Roles)
                && Movies.Equals(other.Movies);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ActorId,ActorName,Movies,Roles);
        }
    }
}
