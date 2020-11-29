using System;

namespace SODV3201_LibMgtSys.Models
{
    public class BookItem
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime Borrowed { get; set; }
        public DateTime Due { get; set; }
    }
}