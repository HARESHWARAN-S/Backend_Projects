using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace LMSModels
{
    public class Copy
    {
        public int copyId {get; set;}
        public string book_Id {get; set;} =string.Empty;
        public CopyStatus book_copy_Status =(CopyStatus)0;

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