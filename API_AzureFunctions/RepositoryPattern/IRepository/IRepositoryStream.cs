using API_AzureFunctions.DTO.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_AzureFunctions.RepositoryPattern.IRepository
{
    public interface IRepositoryStream
    {
        public RestDTO<Models.Stream[]> GetAll_Stream();
    }
}
