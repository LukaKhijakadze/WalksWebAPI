using Microsoft.EntityFrameworkCore;
using WalksAPI.Database.Context;
using WalksAPI.Database.DomainModel;
using WalksAPI.DataModels;
using WalksAPI.DataModels.Domain;
using WalksAPI.Services.IRepository;

namespace WalksAPI.Services.Repository
{
    public class RegionRepository : IRegionRepository
    {
        public WalksDataBaseContext ExerciseContext { get; set; }

        public RegionRepository(WalksDataBaseContext exerciseContext)
        {
            ExerciseContext = exerciseContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await ExerciseContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(int id)
        {
            return await ExerciseContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await ExerciseContext.Regions.AddAsync(region);
            await ExerciseContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(int id, Region region)
        {
            var regionToUpdate = await GetByIdAsync(id);

            if(regionToUpdate == null)
            {
                return null;
            }

            regionToUpdate.RegionImageUrl = region.RegionImageUrl;
            regionToUpdate.Name = region.Name;

            await ExerciseContext.SaveChangesAsync();
            return regionToUpdate;
        }

        public async Task<Region?> DeleteAsync(int id)
        {
            var regionToRemove = await GetByIdAsync(id);

            if (regionToRemove == null)
            {
                return null;
            }

            ExerciseContext.Remove(regionToRemove);
            await ExerciseContext.SaveChangesAsync();
            return regionToRemove;
        }
    }
}
