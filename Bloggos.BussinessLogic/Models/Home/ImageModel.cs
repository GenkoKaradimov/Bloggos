using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.Models.Home
{
    public class ImageModel
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        public string ContentType { get; set; }
    }
}
