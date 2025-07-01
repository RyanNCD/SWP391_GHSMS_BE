using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class GetBlogsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
