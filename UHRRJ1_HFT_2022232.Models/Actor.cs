using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UHRRJ1_HFT_2022232.Models
{
    public class Reader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReaderId { get; set; }

        [Required]
        [StringLength(240)]
        public string ReaderName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
        [JsonIgnore]
        public virtual ICollection<Library> Library { get; set; }
        public Reader()
        {

        }

        public Reader(string line)
        {
            string[] split = line.Split('#');
            ReaderId = int.Parse(split[0]);
            ReaderName = split[1];
        }

        //public override bool Equals(object obj)
        //{
        //    Reader other = obj as Reader;
        //    return ReaderId == other.ReaderId
        //        && ReaderName == other.ReaderName
        //        && Librarys.Equals(other.Librarys)
        //        && Books.Equals(other.Books);
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(ReaderId,ReaderName,Books,Librarys);
        //}
    }
}
