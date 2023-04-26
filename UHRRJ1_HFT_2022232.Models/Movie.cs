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
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [StringLength(240)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public double Income { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        [JsonIgnore]
        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


        public Movie()
        {

        }

        public Movie(string line)
        {
            string[] split = line.Split('#');
            MovieId = int.Parse(split[0]);
            Title = split[1];
            Income = double.Parse(split[2]);
            DirectorId = int.Parse(split[3]);
            Release = DateTime.Parse(split[4].Replace('*', '.'));
            Rating = double.Parse(split[5]);
        }

        public override bool Equals(object obj)
        {
            Movie movie = obj as Movie;
            return MovieId == movie.MovieId
                && Title == movie.Title
                && Income == movie.Income
                && DirectorId == movie.DirectorId
                && Release.Equals(movie.Release)
                && Rating == movie.Rating;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MovieId,Title,Income,DirectorId,Release,Rating);
        }
    }

}
