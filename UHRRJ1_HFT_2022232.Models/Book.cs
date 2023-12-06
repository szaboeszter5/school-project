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

        public virtual ICollection<BookStore> BookStores { get; set; }


        public Book()
        {

        }

        public Book(string line)
        {
            string[] split = line.Split(',');
            BookId = int.Parse(split[0]);
            Title = split[1];
            Price = double.Parse(split[2]);
            Rating = double.Parse(split[3]);
            Release = DateTime.Parse(split[4].Replace('*', '.'));
            AuthorId = int.Parse(split[5]);
        }

        public override bool Equals(object obj)
        {

            if (obj != null && obj is Book Book)
                return BookId == Book.BookId
                    && Title.Equals(Book.Title)
                    && Price == Book.Price
                    && AuthorId == Book.AuthorId
                    && Rating == Book.Rating;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookId, Title, Price, AuthorId, Rating);
        }

        public override string ToString()
        {
            return Title + " (" + Release.Year + ")";
        }
    }

}
