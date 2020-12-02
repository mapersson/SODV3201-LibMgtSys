using System;

namespace SODV3201_LibMgtSys.Models
{
    public class BookItem
    {
        public Guid ID { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
    }
}