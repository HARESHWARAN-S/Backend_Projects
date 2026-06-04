using System.Collections.Generic;
using LMSModels;
using LMSBusinessLogicLayer.Exceptions;
using LMSDataAccessLayer.Repositories;
using LMSDataAccessLayer.Interfaces;
using LMSBusinessLogicLayer.Interfaces;

namespace LMSBusinessLogicLayer.Services
{
    public class FineService : IFineService
    {
        private readonly FineRepository _fineRepository;

        List<Fine>? finelist;
        public FineService(FineRepository fineRepo)
        {
            _fineRepository = fineRepo;
            finelist = _fineRepository.GetAll();
        }
        public void viewUnpaidFine(int memberId)
        {
            foreach(var fine in finelist)
            {
                if((fine.member_id==memberId) && (fine.due_status==0))
                {
                    Console.WriteLine(fine);
                    Console.WriteLine("--------------------------");
                }
            }
        }
        public void payFine(int borrowId)
        {
            int payment_flag=0;
            foreach(var fine in finelist)
            {
                if(fine.Borrow_id==borrowId)
                {
                    fine.due_status=(DueStatus)1;
                    payment_flag=1;
                    break;
                }
            }
            if(payment_flag==0)
            {
                throw new InvalidBorrowIdException();
            }
            else
            {
                Console.WriteLine("Payment done successfully");
            }
        }
        public void finePaymentHistory(int memberId)
        {
            foreach(var fine in finelist)
            {
                if((fine.member_id==memberId) && (fine.due_status==(DueStatus)1))
                {
                    Console.WriteLine(fine);
                    Console.WriteLine("--------------------------");
                }
            }
        }
        public void pendingFineList()
        {
            foreach(var fine in finelist)
            {
                if(fine.due_status==0)
                {
                    Console.WriteLine(fine);
                    Console.WriteLine("--------------------------");
                }
            }
        }
    }
}