using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.Database.Entities
{
    public class Map
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DescriptionHTML { get; set; }

        #region Navigational Properties

        public ICollection<MapLink> MapLinks { get; set; } = new List<MapLink>();

        #endregion
    }
}
