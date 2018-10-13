using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using TestSegmento.BL.API.Models;

namespace TestSegmento.BL.API
{
    public class CustomVKApi
    {
        private const string apiVer = "5.85";
        private string AccessToken { get; set; }

        public CustomVKApi(string token)
        {
            AccessToken = token;
        }

        public async Task<WallPostItem[]> WallGetAsync(long userId, int count)
        {
            var base_uri = "https://api.vk.com/method/wall.get?";
            var @params = new WallGetParams()
            {
                owner_id = userId.ToString(),
                offset = 0,
                count = count,
                filter = "owner"
            };
            VKResponse<WallGetResponse> responseModel;
            using (HttpClient http = new HttpClient())
            {
                List<WallPostItem> items = new List<WallPostItem>();

                double x = 1;
                var requestsNumber = 1;
                if (count > 100)
                {
                    x = count / 100;
                    requestsNumber = (int)Math.Ceiling(x);
                    @params.count = 100;
                }
                for (int i = 0; i < requestsNumber; i++)
                {
                    @params.offset = i * 100;
                    var response = await http.GetAsync(base_uri + GetUriParamsFromObject(@params));
                    var responseContentString = await response.Content.ReadAsStringAsync();
                    CheckErrorResponse(responseContentString);
                    responseModel = JsonConvert.DeserializeObject<VKResponse<WallGetResponse>>(responseContentString);
                    if (!responseModel.response.items.Any())
                    {
                        i = requestsNumber;
                    }
                    items.AddRange(responseModel.response.items);
                }
                return items.ToArray();
            }
        }

        public async Task<User> UserGetAsync(string userId)
        {
            var base_uri = "https://api.vk.com/method/users.get?";
            var @params = new UsersGetParams()
            {
                user_ids = new string[] { userId }
            };
            VKResponse<User[]> responseModel;
            using (HttpClient http = new HttpClient())
            {
                var response = await http.GetAsync(base_uri + GetUriParamsFromObject(@params));
                var responseContentString = await response.Content.ReadAsStringAsync();
                CheckErrorResponse(responseContentString);
                responseModel = JsonConvert.DeserializeObject<VKResponse<User[]>>(responseContentString);
            }
            return responseModel.response.FirstOrDefault();
        }

        private void CheckErrorResponse(string content)
        {
            VKErrorResponse errorResponse;
            try
            {
                errorResponse = JsonConvert.DeserializeObject<VKErrorResponse>(content);
            }
            catch { return; }

            if (errorResponse?.error != null) throw new VKException(errorResponse.error.error_msg);
        }

        private string GetUriParamsFromObject(object obj)
        {
            Type objType = obj.GetType();
            var queryParams = objType.GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(QueryIgnore)) && p.GetValue(obj) != null)
                .Select(prop => 
                    {
                        Object finalValue;
                        var value = prop.GetValue(obj);
                        if (prop.PropertyType.IsArray)
                        {
                            var arr = value as object[];
                            finalValue = String.Join(",", arr);
                        }
                        else
                        {
                            finalValue = value.ToString();
                        }
                        return $"{prop.Name}={finalValue.ToString()}";
                    });

            var queryParamsString = string.Join("&", queryParams) + $"&access_token={AccessToken}&v={apiVer}";
            return queryParamsString;
        }
    }

    
}