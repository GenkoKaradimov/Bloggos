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
            switch (id)
            {
                case 1:
                    return new ArticleModel
                    {
                        Id = 1,
                        Title = "Тригонометрия",
                        HtmlContent = "<p>тригонометрични формули</p>"
                    };
                case 2:
                    return new ArticleModel
                    {
                    };
                default:
                    throw new NotImplementedException();
            }
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
                                DestinationId = 4,
                                Title = "Електричество",
                                Description = "Всички таблици, схеми и диаграми от всички справочници.",
                                ImageSource = "/images/kotka.jpg",
                                DestinationType = LinkDestination.MapPage
                            },
                            new LinkModel()
                            {
                                DestinationId = 3,
                                Title = "Механични",
                                Description = "Механични справочници",
                                ImageSource = "/images/kotka.jpg",
                                DestinationType = LinkDestination.MapPage
                            },
                            new LinkModel(){
                                DestinationId = 2,
                                Title = "Математика",
                                Description = "Всички таблици, схеми и диаграми от всички справочници",
                                ImageSource = "/images/kotka.jpg",
                                DestinationType = LinkDestination.MapPage
                            },
                        }
                    };
                case 2:
                    return new MapPageModel()
                    {
                        Id = 2,
                        Title = "Математика",
                        Description = "Справочни данни математика",
                        Destinations = new List<LinkModel>
                        {
                            new LinkModel(){
                                DestinationId = 1,
                                Title = "Тригонометрия",
                                Description = "Всички таблици, схеми и диаграми от всички справочници.",
                                ImageSource = "/images/kotka.jpg",
                                DestinationType = LinkDestination.ArticlePage
                            }
                        }
                    };
                case 3:
                    return new MapPageModel()
                    {

                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
