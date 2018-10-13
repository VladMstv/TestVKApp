using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestSegmento.BL.API.Models;

namespace TestSegmento.BL.API.Interfaces
{
    public interface IVKApiManager
    {
        Task<List<WallPostItem>> GetVKWallPostsAsync(string id);
    }
}