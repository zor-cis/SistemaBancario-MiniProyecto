using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _con;
        public AccountRepository(ApplicationDbContext con)
        {
            _con = con;
        }
        public async Task AddAccount(Account account)
        {
            await _con.Accounts.AddAsync(account);
            await _con.SaveChangesAsync();
        }
    }
}
