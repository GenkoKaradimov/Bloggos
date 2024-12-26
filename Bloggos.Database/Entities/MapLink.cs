using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.Database.Entities
{
    public class MapLink
    {
        public int Id { get; set; }

        public int OrderWeight { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public int DestinationId { get; set; }

        public MapLinkDestinationType DestinationType { get; set; }

        #region Navigational Properties

        public int MapId { get; set; }
        public Entities.Map Map { get; set; }

        #endregion
    }
}
