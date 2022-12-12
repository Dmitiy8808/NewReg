using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;
using Entities.FileFeatures;

namespace Client.HttpRepository
{
    public interface IRequestFileHttpRepository
    {
        Task<RequestFileReadDto> UploadRequestFile(MultipartFormDataContent content, RequestFileFeatures requestFileFeatures);
        Task DeleteRequestFile(Guid id);
        Task<List<RequestFileReadDto>> GetRequestFiles(Guid id);
    }
}