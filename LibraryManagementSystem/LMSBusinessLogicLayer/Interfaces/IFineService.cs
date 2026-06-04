using System.Collections.Generic;
using LMSModels;

namespace LMSBusinessLogicLayer.Interfaces
{
    public interface IFineService
    {
        void viewUnpaidFine(int memberId);
        void payFine(int borrowId);
        void finePaymentHistory(int memberId);
        void pendingFineList();
    }
}