using AutoMapper;
using Bank_System.DTO;
using Bank_System.Models;

namespace Bank_System
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
            CreateMap<RegisterDTO, Account>();
        }
    }
}
