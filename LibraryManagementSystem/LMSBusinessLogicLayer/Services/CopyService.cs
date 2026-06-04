using System.Collections.Generic;
using LMSModels;
using LMSBusinessLogicLayer.Exceptions;
using LMSDataAccessLayer.Repositories;
using LMSDataAccessLayer.Interfaces;
using LMSBusinessLogicLayer.Interfaces;

namespace LMSBusinessLogicLayer.Services
{
    public class CopyService : ICopyService
    {
        readonly CopyRepository _copyRepository;
        readonly BookRepository _bookRepository;
        public CopyService(BookRepository bookRepo,CopyRepository copyRepo)
        {
            _bookRepository = bookRepo;
            _copyRepository = copyRepo;
        }
        public void addCopy(string bookid,bool added)
        {
            if(added)
            {
                _copyRepository.Add(new Copy(bookid));
            }
            else 
            {
            var books = _bookRepository.GetAll();
            int flag=0;
            foreach(var b in books)
            {
                if(b.book_Id==bookid)
                {
                    _copyRepository.Add(new Copy(bookid));
                    flag=1;
                    break;
                }
            }
            if(flag==0)
            {
                throw new NoBookFoundException();
            }
            }
        }
        public void MarkCopyStatus(int copyId,int status)
        {
            var copylist = _copyRepository.GetAll();
            foreach(var copy in copylist)
            {
                if(copy.copyId==copyId)
                {
                    copy.book_copy_Status=(CopyStatus)status;
                }
            }
        }
    }
}