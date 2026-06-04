using System.Collections.Generic;
using Library.Models;
using Library.Models.DTOs;

namespace Library.Services
{
    public interface IFineService
    {
        List<FineReadDto>? viewUnpaidFine(int memberId);
        FineReadDto? payFine(int borrowId);
        List<FineReadDto>? finePaymentHistory(int memberId);
        List<FineReadDto>? pendingFineList();
    }
}