using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaApi.ApiEntities
{
    public class ImageEntities
    {


        public class Rootobject
        {
            public string _type { get; set; }
            public int totalCount { get; set; }
            public Value[] value { get; set; }
        }

        public class Value
        {
            public string url { get; set; }
            public int height { get; set; }
            public int width { get; set; }
            public string thumbnail { get; set; }
            public int thumbnailHeight { get; set; }
            public int thumbnailWidth { get; set; }
            public object base64Encoding { get; set; }
            public string name { get; set; }
            public string title { get; set; }
            public Provider provider { get; set; }
            public string imageWebSearchUrl { get; set; }
            public string webpageUrl { get; set; }
        }

        public class Provider
        {
            public string name { get; set; }
            public string favIcon { get; set; }
            public string favIconBase64Encoding { get; set; }
        }


    }
}