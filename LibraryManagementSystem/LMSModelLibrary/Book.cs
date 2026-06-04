using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace LMSModels
{
    public class Book
    {
        public string book_Id {get; set;} =string.Empty;
        public string book_name {get; set;} =string.Empty;
        public string book_author {get; set;} =string.Empty;
        public string book_category {get; set;} =string.Empty;

    public Book()
    {
        
    }

    public Book(string id,string name, string author, string category)
    {
        book_Id=id;
        book_name=name;
        book_author=author;
        book_category=category;
    }
    public override string ToString()
    {
        return $"Book ID : {book_Id}\nBook Name : {book_name}\nBook Author : {book_author}\nBook Category : {book_category}\n";
    } 

    }
}