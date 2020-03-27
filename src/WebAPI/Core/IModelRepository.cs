using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Core
{
    public interface IModelRepository
    {
        Task<Model> GetModel(int id);
    }
}