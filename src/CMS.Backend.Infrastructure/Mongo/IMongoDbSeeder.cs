using System.Threading.Tasks;

namespace BusinessFile.Backend.Infrastructure.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}