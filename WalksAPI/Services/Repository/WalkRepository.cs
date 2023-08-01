using Microsoft.EntityFrameworkCore;
using WalksAPI.Database.Context;
using WalksAPI.Database.DomainModel;
using WalksAPI.DataModels;
using WalksAPI.DataModels.Domain;
using WalksAPI.Services.IRepository;

namespace WalksAPI.Services.Repository
{
    public class WalkRepository : IWalkRepository
    {
        public WalksDataBaseContext ExerciseContext { get; set; }

        public WalkRepository(WalksDataBaseContext exerciseContext)
        {
            ExerciseContext = exerciseContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await ExerciseContext.Walks.AddAsync(walk);
            await ExerciseContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(int id)
        {
            var walk = await ExerciseContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            ExerciseContext.Walks.Remove(walk);
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery,
                string? sortBy = null, bool isAscending = true,
                int pageNumber = 1, int pageSize = 20)
        {
            var walks = ExerciseContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            // FILTERING
            if(filterOn != null && filterQuery != null)
            {
                if (filterOn.ToLower().Equals("name"))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery.ToLower()));
                }
            }

            // SORTING 
            if(sortBy != null)
            {
                if(sortBy.ToLower().Equals("name"))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                if (sortBy.ToLower().Equals("length"))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.Name);
                }
            }

            // PAGINATION
            var skinResult = (pageNumber - 1) * pageSize;

            var resulklt = await walks.Skip(skinResult).Take(pageSize).ToListAsync();
            ExerciseContext.Dispose();
            return resulklt;
        }

        public async Task<Walk?> GetByIdAsync(int id)
        {
            return await ExerciseContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(int id, Walk walk)
        {
            var walkToUpdate = await GetByIdAsync(id);

            if(walkToUpdate == null)
            {
                return null;
            }

            walkToUpdate.Difficulty = walk.Difficulty;
            walkToUpdate.Id = id;
            walkToUpdate.DifficultyId = walk.DifficultyId;
            walkToUpdate.RegionId= walk.RegionId;
            walkToUpdate.WalkImageUrl = walk.WalkImageUrl;
            walkToUpdate.Region = walk.Region;
            walkToUpdate.Description = walk.Description;
            walkToUpdate.Name = walk.Name;
            walkToUpdate.LengthInKm = walk.LengthInKm;

            await ExerciseContext.SaveChangesAsync();
            return walkToUpdate;
        }
    }
}
