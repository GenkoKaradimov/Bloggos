using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.Models.Blog
{
    public class MapPageModel
    {
        /// <summary>
        /// Represents the date of mapping page, that is used to navigate user to specific article.
        /// </summary>
        public int Id { get; set; }

        public string Title { get; set; }

        public string DescriptionHTML { get; set; }

        public IEnumerable<LinkModel> Destinations { get; set; }
    }
}
