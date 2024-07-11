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
        #endregion

        #region Transaction
        CreateMap<TransationDTO, Transaction>().ReverseMap();   
        CreateMap<Account,TransactionReturnDto >().ReverseMap();   
        #endregion
    }
}