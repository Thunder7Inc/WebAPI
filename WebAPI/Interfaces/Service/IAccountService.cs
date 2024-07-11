
using WebAPI.Models.DTOs;
namespace WebAPI.Interfaces.Service;
public interface IAccountService
{
    public Task<AccountReturnDTO> Add(AccountDTO account);
    public Task<AccountReturnDTO> Get(int id);
}