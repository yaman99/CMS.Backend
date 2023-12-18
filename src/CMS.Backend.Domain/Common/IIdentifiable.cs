namespace CMS.Backend.Domain.Common
{
    public interface IIdentifiable<T> 
    {
         T Id { get; }
    }
}