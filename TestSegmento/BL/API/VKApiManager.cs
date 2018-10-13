using System.Collections.Generic;
using System.Threading.Tasks;
using TestSegmento.BL.API.Models;

namespace TestSegmento.BL.API
{
    public class VKApiManager
    {
        public string AccessToken { get; set; }

        private CustomVKApi _custVkAPI;
        private CustomVKApi CustomVKAPI
        {
            get
            {
                if (_custVkAPI == null)
                    _custVkAPI = new CustomVKApi(AccessToken);
                return _custVkAPI;
            }
        }

        public VKApiManager(string _accessToken)
        {
            AccessToken = _accessToken;
        }

        public async Task<List<WallPostItem>> GetVKWallPostsAsync(string id)
        {
            List<WallPostItem> wallPostsList = new List<WallPostItem>();
            long UId;

            if (!long.TryParse(id, out UId))
            {
                var user = await GetVKUserAsync(id);
                UId = user.id;
            }
            
            var posts = await CustomVKAPI.WallGetAsync(UId, 1000);

            wallPostsList.AddRange(posts);

            return wallPostsList;
        }

        private async Task<User> GetVKUserAsync(string id)
        {
            return await CustomVKAPI.UserGetAsync( id );
        }
    }
}