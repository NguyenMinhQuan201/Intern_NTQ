using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.Product
{
    public class AddImageRequest
    {
        public int Id { get; set; }
        public List<IFormFile> ProductImageVMs { get; set; }
    }
}
