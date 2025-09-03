using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class ClientRepository : IClientRepository
    {
        private ApplicationDbContext _con;

        public ClientRepository(ApplicationDbContext con)
        {
            _con = con;
        }
        public async Task AddClient(Client client)
        {
            await _con.Client.AddAsync(client);
            await _con.SaveChangesAsync();
        }

        public async Task<Client?> GetClientByEmail(string email)
        {
            return await _con.Client.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client?> GetDocumentTypeId(int documentTypeId)
        {
            return await _con.Client.FirstOrDefaultAsync(c => c.DocumentTypeId == documentTypeId);
        }
    }
}
