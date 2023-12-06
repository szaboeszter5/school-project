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
    public class BookStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookStoreId { get; set; }

        public string BookStoreName { get; set; }

        public int BookId { get; set; }

        public int ReaderId { get; set; }

        public virtual Reader Reader { get;  set; }

        [JsonIgnore]
        public virtual Book Book { get;  set; }

        public BookStore()
        {

        }

        public BookStore(string line)
        {
            string[] split = line.Split(',');
            BookStoreId = int.Parse(split[0]);
            BookStoreName = split[1];
            BookId = int.Parse(split[2]);
            ReaderId = int.Parse(split[3]);
            
        }

        public override bool Equals(object obj)
        {
            BookStore other = obj as BookStore;
            if (other != null)
            {
                return BookStoreId == other.BookStoreId
                    && BookStoreName == other.BookStoreName
                    && BookId == other.BookId
                    && ReaderId == other.ReaderId;
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookStoreId, BookId, ReaderId, BookStoreName);
        }

        public override string ToString()
        {
            return BookStoreId + " " + BookStoreName;
        }
    }
}
