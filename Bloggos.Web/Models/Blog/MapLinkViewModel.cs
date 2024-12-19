using Bloggos.BussinessLogic.Models.Blog;

namespace Bloggos.Web.Models.Blog
{
    public class MapLinkViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int DestinationId { get; set; }

        public string ImageSource { get; set; }

        public LinkDestinationViewModel DestinationType { get; set; }
    }
}
