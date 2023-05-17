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
    public class Library
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LibraryId { get; set; }

        public string LibraryName { get; set; }

        public int BookId { get; set; }

        public int ReaderId { get; set; }

        public virtual Reader Reader { get; private set; }

        [JsonIgnore]
        public virtual Book Book { get; private set; }

        public Library()
        {

        }

        public Library(string line)
        {
            string[] split = line.Split('#');
            LibraryId = int.Parse(split[0]);
            BookId = int.Parse(split[1]);
            ReaderId = int.Parse(split[2]);
            LibraryName = split[3];
        }

        //public override bool Equals(object obj)
        //{
        //    Library other = obj as Library;
        //    return LibraryId == other.LibraryId
        //        && BookId == other.BookId
        //        && ReaderId == other.ReaderId
        //        && Priority == other.Priority
        //        && LibraryName == other.LibraryName
        //        && Reader.Equals(other.Reader)
        //        && Book.Equals(other.Book); 
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(LibraryId,BookId,ReaderId,Priority,LibraryName,Reader,Book);
        //}
    }
}
