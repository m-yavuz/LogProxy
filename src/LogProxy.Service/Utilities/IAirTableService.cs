﻿using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogProxy.Service.Utilities
{
    [Headers("Authorization: Bearer", "Content-Type: application/json")]
    public interface IAirTableService
    {
        [Get("/Messages")]
        Task<DataSet> GetMessages([Query] int maxRecords = 3, [Query] string view = "Grid view");

        [Post("/Messages")]
        Task AddMessages([Body] DataSet data);
    }
}