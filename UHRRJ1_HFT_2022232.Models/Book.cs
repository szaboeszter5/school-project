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
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [StringLength(240)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public double Price { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reader> Readers { get; set; }

        public virtual ICollection<Library> Libraries { get; set; }


        public Book()
        {

        }

        public Book(string line)
        {
            string[] split = line.Split('#');
            BookId = int.Parse(split[0]);
            Title = split[1];
            Price = double.Parse(split[2]);
            AuthorId = int.Parse(split[3]);
            Release = DateTime.Parse(split[4].Replace('*', '.'));
            Rating = double.Parse(split[5]);
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj is Book Book)
        //        return this.BookId == Book.BookId
        //            && this.Title.Equals(Book.Title)
        //            && this.Income == Book.Income
        //            && this.AuthorId == Book.AuthorId
        //            && this.Release.Equals(Book.Release)
        //            && this.Rating == Book.Rating;
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(BookId,Title,Income,AuthorId,Release,Rating);
        //}
    }

}
