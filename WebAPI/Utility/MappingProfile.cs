using AutoMapper;
using WebAPI.Models;
using WebAPI.Models.DTOs;

namespace WebAPI.Utility;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Account
        CreateMap<AccountDTO, Account>().ReverseMap();   
        CreateMap<Account, AccountReturnDTO>().ReverseMap();   
        CreateMap<AccountDTO, AccountReturnDTO>().ReverseMap();   
        #endregion

        #region Transaction
        CreateMap<TransactionDTO, Transaction>().ReverseMap();   
        CreateMap<Transaction,TransactionReturnDto >().ReverseMap();   
        CreateMap<TransactionDTO,TransactionReturnDto >().ReverseMap();   
        #endregion
    }
}