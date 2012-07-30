using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Tetris.WebApp.Dto
{
    public static class JsonExtensions
    {
        public static string ToJson(this CommunicationMessageBase dto)
        {
            return JsonConvert.SerializeObject(dto, Formatting.Indented);
        }
    }
}
