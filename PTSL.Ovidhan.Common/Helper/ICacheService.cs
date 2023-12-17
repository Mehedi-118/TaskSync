using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Common.Helper
{
    public interface ICacheService
    {
        Task SetAsync(string key, object value);
        Task<byte[]> GetAsync(string key);
    }
}
