using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.Database.Entities
{
    public class Image
    {
        public int Id { get; set; }

        [MaxLength(1073741824)] // 1 GB in bytes
        public byte[] ImageData { get; set; }

        [MaxLength(300)]
        public string ContentType { get; set; }
    }
}
