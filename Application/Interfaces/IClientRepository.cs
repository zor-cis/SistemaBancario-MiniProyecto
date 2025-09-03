using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetClientByEmail(string email);
        Task<Client?> GetDocumentTypeId(int documentTypeId);
        Task AddClient(Client client);
    }
}
