using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSegmento.BL.API.Models
{
    public class VKResponse<T>
    {
        public T response { get; set; }
    }
    public class VKErrorResponse
    {
        public VKError error { get; set; }
    }

    public class VKError
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
    }

    #region wallget
    public class WallGetParams
    {
        public string owner_id { get; set; }
        public long? offset { get; set; }
        public long count { get; set; }
        public string filter { get; set; }
    }
    
    public class WallGetResponse
    {
        public long count { get; set; }
        public WallPostItem[] items { get; set; }
    }
    
    public class WallPostItem
    {
        public int id { get; set; }
        public int date { get; set; }
        public Likes likes { get; set; }
    }
    #endregion

    #region users
    public class UsersGetParams
    {
        public string[] user_ids { get; set; }
        public string[] fields { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string deactivated { get; set; }
    }
    #endregion

    public class Likes
    {
        public int count { get; set; }
    }

    public class QueryIgnore : Attribute { }
}