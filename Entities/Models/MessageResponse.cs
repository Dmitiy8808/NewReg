using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MessageResponse
    {
         public bool Success { get; set; }

        public string? Message { get; set; }

        public ResponseData? Data { get; set; }
    }
}