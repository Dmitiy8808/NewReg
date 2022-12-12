using System.Net.Http.Headers;
using AutoMapper;
using Entities.DTOs;
using Entities.FileFeatures;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;

namespace Server.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public FileController(IFileRepository fileRepository, IMapper mapper)
        {
            _mapper = mapper;
            _fileRepository = fileRepository;
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] RequestFileFeatures requestFileFeatures)
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    RequestFile requestFile = new RequestFile();
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    byte[] fileData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = binaryReader.ReadBytes((int)file.Length);
                    }
                    // установка массива байтов
                    requestFile.Data = fileData;
                    requestFile.Name = fileName; 
                    requestFile.RequestAbonentId = requestFileFeatures.RequestAbonentId;
                    requestFile.TypeId = requestFileFeatures.TypeId;
                    await _fileRepository.SaveFile(requestFile);
                    var requestFileReadDto = _mapper.Map<RequestFileReadDto>(requestFile);

                    return CreatedAtAction(nameof(GetRequestFile), new { id = requestFile.Id }, requestFileReadDto);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRequestFile(Guid id)
        {
            var fileFromRepo = await _fileRepository.GetFile(id);
            if(fileFromRepo == null)
            {
                return NotFound();
            }

            await _fileRepository.DeleteFile(fileFromRepo); 

            return NoContent();
        }

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<RequestFile>> GetRequestFile(Guid id)
        {
            var file = await _fileRepository.GetFile(id); 
            if (file != null)
            {
                return Ok(file);
            }
            return NotFound();
        }

        [Route("list/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<List<RequestFile>>> GetRequestFileGuid(Guid id)
        {
            var fileList = await _fileRepository.GetFilesByRequestId(id); 
            if (fileList != null)
            {
                return Ok((_mapper.Map<List<RequestFileReadDto>>(fileList)));
            }
            return NotFound();
        }
    }
}