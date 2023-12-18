using System.Threading.Tasks;

namespace CMS.Backend.Infrastructure.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}