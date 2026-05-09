using MyOfficeACPD.Models;

namespace MyOfficeACPD.Repositories
{
    public interface IAcpdRepository
    {
        Task<IEnumerable<MyOfficeAcpd>> GetAllAsync();
        Task<MyOfficeAcpd?> GetByIdAsync(string id);
        Task<MyOfficeAcpd> CreateAsync(CreateAcpdRequest request);
        Task<bool> UpdateAsync(string id, UpdateAcpdRequest request);
        Task<bool> DeleteAsync(string id);
    }
}
