using ETicaret.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class MetaContent :BaseEntity
    {
        public string? MetaKeywords { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
