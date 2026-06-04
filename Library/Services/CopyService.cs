using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;
using LMSBusinessLogicLayer.Exceptions;
using Library.Repositories;

namespace Library.Services
{
    public class CopyService : ICopyService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICopyRepository _copyRepository;

        public CopyService(IBookRepository bookRepo, ICopyRepository copyRepo)
        {
            _bookRepository = bookRepo;
            _copyRepository = copyRepo;
        }
        public List<CopyReadDto>? addCopy(string bookid,bool added)
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
            return _copyRepository.GetAll()
            .Select(c => new CopyReadDto
            {
                copyId = c.copyId,
                book_Id = c.book_Id,
                status = c.book_copy_Status.ToString()
            })
            .ToList();
        }
        public CopyReadDto? MarkCopyStatus(int copyId,int status)
        {
            if((status<0) || (status>2))
            {
                throw new InvalidStatusException();
            }
            var copylist = _copyRepository.GetAll();
            foreach(var copy in copylist)
            {
                if(copy.copyId==copyId)
                {
                    copy.book_copy_Status=(CopyStatus)status;
                    _copyRepository.Update(copy);
                    return new CopyReadDto
                    {
                        copyId = copy.copyId,
                        book_Id = copy.book_Id,
                        status = copy.book_copy_Status.ToString()
                    };
                }
            }
            return null;
        }
    }
}