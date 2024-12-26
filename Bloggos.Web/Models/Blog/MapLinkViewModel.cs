using Bloggos.BussinessLogic.Models.Blog;

namespace Bloggos.Web.Models.Blog
{
    public class MapLinkViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public int DestinationId { get; set; }

        public LinkDestinationViewModel DestinationType { get; set; }

        public int MapId { get; set; }

        public int OrderWeight { get; set; }

        public IDictionary<int, string> DestinationOptions { get; set; }
    }
}
