using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;

namespace Library.Services
{
    public interface ICopyService
    {
        List<CopyReadDto>? addCopy(string bookid,bool Added);
        CopyReadDto? MarkCopyStatus(int copyId,int status);
    }
}