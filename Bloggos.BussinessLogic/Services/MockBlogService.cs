using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.Services
{
    public class MockBlogService : IBlogService
    {
        public MockBlogService() 
        {
        }

        public async Task<ArticleModel> GetArticleAsync(int id)
        {
            return null;
        }

        public async Task<MapPageModel> GetMapPageAsync(int id)
        {
            switch (id)
            {
                case 1:
                    return new MapPageModel()
                    {
                        Id = 1,
                        Title = "Справочник",
                        Description = "Можете да намерите точни справочни данни.",
                        Destinations = new List<LinkModel>
                        {
                            new LinkModel(){
                                DestinationId = 2,
                                Title = "Електричество",
                                Description = "Всички таблици, схеми и диаграми от всички справочници",
                            },
                            new LinkModel()
                            {
                                DestinationId = 3,
                                Title = "Механични",
                                Description = "Механични справочници",
                            },
                            new LinkModel(){
                                DestinationId = 4,
                                Title = "Математика",
                                Description = "Всички таблици, схеми и диаграми от всички справочници",
                            },
                        }
                    };
                case 2:
                    return new MapPageModel()
                    {

                    };
                case 3:
                    return new MapPageModel()
                    {

                    };
                default:
                    return null;
            }
        }
    }
}
