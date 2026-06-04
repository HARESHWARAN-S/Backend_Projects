using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;
using LMSBusinessLogicLayer.Exceptions;
using Library.Repositories;

namespace Library.Services
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _fineRepository;

        public FineService(IFineRepository fineRepo)
        {
            _fineRepository = fineRepo;
        }
        public List<FineReadDto>? viewUnpaidFine(int memberId)
        {
            var finelist = _fineRepository.GetAll();
            List<FineReadDto> unpaidFine = new List<FineReadDto>();
            foreach(var fine in finelist)
            {
                if((fine.member_id==memberId) && (fine.due_status==0))
                {
                    unpaidFine.Add(new FineReadDto
                    {
                        Borrow_id = fine.Borrow_id,
                        member_id = fine.member_id,
                        amount = fine.amount,
                        status = fine.due_status.ToString()
                    });
                }
            }
            return unpaidFine;
        }
        public FineReadDto? payFine(int borrowId)
        {
            int payment_flag=0;
            Fine paidFine = new Fine();
            var finelist = _fineRepository.GetAll();
            foreach(var fine in finelist)
            {
                if(fine.Borrow_id==borrowId)
                {
                    fine.due_status=(DueStatus)1;
                    _fineRepository.Update(fine);
                    payment_flag=1;
                    paidFine=fine;
                    break;
                }
            }
            if(payment_flag==0)
            {
                throw new InvalidBorrowIdException();
            }
            else
            {
                return new FineReadDto
                {
                    Borrow_id = paidFine.Borrow_id,
                    member_id = paidFine.member_id,
                    amount = paidFine.amount,
                    status = paidFine.due_status.ToString()
                };
            }
        }
        public List<FineReadDto>? finePaymentHistory(int memberId)
        {
            var finelist = _fineRepository.GetAll();
            List<FineReadDto> fineHistory = new List<FineReadDto>();
            foreach(var fine in finelist)
            {
                
                if((fine.member_id==memberId) && (fine.due_status==(DueStatus)1))
                {
                    fineHistory.Add(new FineReadDto
                    {
                        Borrow_id = fine.Borrow_id,
                        member_id = fine.member_id,
                        amount = fine.amount,
                        status = fine.due_status.ToString()
                    });
                }
            }
            return fineHistory;
        }
        public List<FineReadDto>? pendingFineList()
        {
            var finelist = _fineRepository.GetAll();
            List<FineReadDto> pendingFine = new List<FineReadDto>();
            foreach(var fine in finelist)
            {
                if(fine.due_status==0)
                {
                    pendingFine.Add(new FineReadDto
                    {
                        Borrow_id = fine.Borrow_id,
                        member_id = fine.member_id,
                        amount = fine.amount,
                        status = fine.due_status.ToString()
                    });
                }
            }
            return pendingFine;
        }
    }
}