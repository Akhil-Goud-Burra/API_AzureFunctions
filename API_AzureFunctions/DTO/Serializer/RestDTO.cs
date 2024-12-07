using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_AzureFunctions.DTO.Serializer
{
    public class RestDTO<T>
    {
        public T Data { get; set; } = default!;
    }
}
