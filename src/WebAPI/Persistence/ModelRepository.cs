using System.Threading.Tasks;
using Vega.Core;
using Vega.Core.Models;

namespace Vega.Persistence
{
    public class ModelRepository : IModelRepository
    {
        private readonly VegaDbContext context;

        public ModelRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Model> GetModel(int id)
        {
            return await context.Models.FindAsync(id);
        }
    }
}