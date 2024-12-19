using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.Models.Blog
{
    public class LinkModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public int DestinationId { get; set; }

        public LinkDestination DestinationType { get; set; }
    }
}
