using AutoMapper;
using Intern.NTQ.Domain.Models.Product;
using Intern.NTQ.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Models.Mapper
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ProductImageVM, ProductImg>();
            CreateMap<ProductImg, ProductImageVM>();
        }
    }
}
