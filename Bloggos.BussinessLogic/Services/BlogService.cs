using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Models.Blog;
using Bloggos.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.Services
{
    public class BlogService : IBlogService
    {
        private readonly BloggosDbContext _context;

        public BlogService(BloggosDbContext context)
        {
            _context = context;
        }

        #region Articles

        public async Task<List<ArticleModel>> AllArticles()
        {
            var articles = _context.Articles.AsQueryable();

            var resp = await articles.Select(x => new ArticleModel()
            {
                Id = x.Id,
                Title = x.Title,
                HtmlContent = x.HtmlContent
            }).ToListAsync();

            return resp;
        }

        public async Task<ArticleModel> GetArticleAsync(int id)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(x => x.Id == id);
            if (article == null) throw new ArgumentException("Article is not found!");

            var model = new Models.Blog.ArticleModel()
            {
                Id = article.Id,
                Title = article.Title,
                HtmlContent = article.HtmlContent
            };

            return model;
        }

        public async Task<ArticleModel> AddArticleAsync(ArticleModel model)
        {
            var article = new Database.Entities.Article()
            {
                Title = model.Title,
                HtmlContent= model.HtmlContent
            };

            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();

            model.Id = article.Id;
            return model;
        }

        public async Task<ArticleModel> EditArticleAsync(ArticleModel model)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (article == null) throw new ArgumentException("Article is not found!");

            article.Title = model.Title;
            article.HtmlContent = model.HtmlContent;

            await _context.SaveChangesAsync();

            return model;
        }

        #endregion

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
