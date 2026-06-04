using System.Collections.Generic;
using LMSModels;

namespace LMSBusinessLogicLayer.Interfaces
{
    public interface ICopyService
    {
        void addCopy(string bookid,bool Added);
        void MarkCopyStatus(int copyId,int status);
    }
}