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
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(240)]
        public string AuthorName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new HashSet<Book>();
        }

        public Author(string line)
        {
            string[] split = line.Split('#');
            AuthorId = int.Parse(split[0]);
            AuthorName = split[1];
            Books = new HashSet<Book>();
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj is Author other)
        //        return AuthorId == other.AuthorId
        //            && AuthorName == other.AuthorName
        //            && Books.Equals(other.Books);
        //    return false;
        //}
        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(AuthorId,AuthorName,Books);
        //}
    }

}
