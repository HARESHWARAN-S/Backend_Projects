using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Models
{
    public class Copy
    {
        public int copyId {get; set;}
        public string book_Id {get; set;} =string.Empty;
        public Book? Book { get; set; }
        public List<Borrow>? Borrows { get; set; } =  new List<Borrow>();
        public CopyStatus book_copy_Status {get; set;} = CopyStatus.Available;

        public Copy()
        {
            
        }

        public Copy(string Id)
        {
            book_Id=Id;
        }
        public override string ToString()
        {
            return $"Copy Id : {copyId}\nBook Id : {book_Id}\nCopy Status : {(CopyStatus)book_copy_Status}\n";
        }
    }
}