namespace Bloggos.Web.Models.Blog
{
    /// <summary>
    /// Represents the date of mapping page, that is used to navigate user to specific article.
    /// </summary>
    public class MapViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Destinations { get; set; }
    }
}
