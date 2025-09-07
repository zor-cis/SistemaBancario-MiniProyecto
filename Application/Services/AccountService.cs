using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Factories;
using System.Reflection.Metadata.Ecma335;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _repo;
        public readonly IClientRepository _client;
        public readonly IUserContextService _userContex;

        public AccountService(IAccountRepository repo, IUserContextService userContex, IClientRepository client)
        {
            _repo = repo;
            _userContex = userContex;
            _client = client;
        }

        public async Task<AccountResponse> CreateAccount(AccountCreate dto)
        {
            var user = await _client.GetClientById(dto.idClient);
            if(user == null)
                throw new Exception("No se encontro el cliente");
            var account = FactoryAccount.Create
                (
                    accountNumber: dto.AccountNumber,
                    TypeAcount: dto.Type,
                    idClient: dto.idClient,
                    holderAccount: user.FullName(),
                    client: user
                );

            await _repo.AddAccount(account);
            return new AccountResponse
            {
                AccountNumber = account.AccountNumber,
                TypeAccount = (int)account.TypeAccount,
                HolderAccount = account.HolderAccount,
                Balance = account.Balance,
                CreatedAt = account.CreatedAt,
                isActive = account.isActive
            };
        }
    }
}
