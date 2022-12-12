using Entities.Models;

namespace Server.Repository
{
    public interface IFileRepository
    {
        Task<List<RequestFile>> GetFilesByRequestId(Guid requestId);
        Task SaveFile(RequestFile file);
        Task<RequestFile> GetFile(Guid id);
        Task DeleteFile(RequestFile file);
    }
}