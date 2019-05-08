using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarsWars.Services.Models
{
    public class CharacterResponse : BaseResponse
    {
        public object Data { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}