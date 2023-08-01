using Microsoft.AspNetCore.Mvc;
using WalksAPI.Database.DomainModel;
using WalksAPI.DataModels.Domain;

namespace WalksAPI.Services.IRepository
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 20);
        Task<Walk?> GetByIdAsync(int id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(int id, Walk walk);
        Task<Walk?> DeleteAsync(int id);
    }
}
