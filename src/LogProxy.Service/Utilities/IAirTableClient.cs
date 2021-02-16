using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogProxy.Service.Utilities
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IAirTableClient
    {
        [Get("/Messages")]
        Task<FetchDataSet> GetMessages([Query] int maxRecords = 3, [Query] string view = "Grid view");

        [Post("/Messages")]
        Task<FetchDataSet> AddMessages([Body] NewDataSet data);
    }
}
