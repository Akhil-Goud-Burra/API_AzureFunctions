using API_AzureFunctions.DTO.Serializer;
using API_AzureFunctions.ErrorHandling.CustomExceptionResponse;
using API_AzureFunctions.Models;
using API_AzureFunctions.RepositoryPattern.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_AzureFunctions.RepositoryPattern.IRepositoryImplementation
{
    public class IRepositoryStreamImplementation : IRepositoryStream
    {
        private readonly MyDbContext _appDbContext;
        public IRepositoryStreamImplementation(MyDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public RestDTO<Models.Stream[]> GetAll_Stream()
        {
            try
            {
                var Fetch_Data = _appDbContext.Streams.ToArray();

                return new RestDTO<Models.Stream[]>()
                {
                    Data = Fetch_Data,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Type: {ex.GetType()}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                throw new ApiException("An error occurred while fetching the request.", 500);
            }
        }
    }
}
