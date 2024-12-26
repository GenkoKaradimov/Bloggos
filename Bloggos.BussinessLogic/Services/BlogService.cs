using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Models.Blog;
using Bloggos.Database;
using Bloggos.Database.Entities;
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
                HtmlContent = model.HtmlContent
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

        #region Map`s pages

        public async Task<MapPagesModel> GetMapPagesAsync()
        {
            var pages = await _context.Maps.Include(m => m.MapLinks).ToListAsync();

            var resp = new MapPagesModel();
            resp.MapPages = pages.Select(x => new MapPageModel()
            {
                Id = x.Id,
                Title = x.Title,
                DescriptionHTML = x.DescriptionHTML,
                Destinations = x.MapLinks.OrderBy(y => y.OrderWeight).Select(y => new LinkModel()
                {
                    Title = y.Title,
                    Description = y.Description,
                    ImageSource = y.ImageSource,
                    DestinationId = y.DestinationId,
                    DestinationType = (LinkDestination)(int)y.DestinationType
                })
            }).ToList();

            return resp;
        }

        public async Task<MapPageModel> GetMapPageAsync(int id)
        {
            var map = await _context.Maps.Include(m => m.MapLinks).SingleOrDefaultAsync(m => m.Id == id);

            if (map == null) throw new ArgumentException("Map not found!");

            var model = new MapPageModel()
            {
                Id = map.Id,
                Title = map.Title,
                DescriptionHTML = map.DescriptionHTML,
                Destinations = map.MapLinks.OrderBy(y => y.OrderWeight).Select(y => new LinkModel()
                {
                    Id = y.Id,
                    Title = y.Title,
                    ImageSource = y.ImageSource,
                    Description = y.Description,
                    DestinationId = y.DestinationId,
                    DestinationType = (LinkDestination)(int)y.DestinationType
                })
            };

            return model;
        }

        public async Task<MapPageModel> AddMapPageAsync(MapPageModel model)
        {
            var map = new Database.Entities.Map()
            {
                Title = model.Title,
                DescriptionHTML = model.DescriptionHTML
            };

            await _context.Maps.AddAsync(map);
            await _context.SaveChangesAsync();

            model.Id = map.Id;
            return model;
        }

        public async Task<MapPageModel> EditMapPageAsync(MapPageModel model)
        {
            var map = await _context.Maps.Include(m => m.MapLinks).SingleOrDefaultAsync(m => m.Id == model.Id);
            if (map == null) throw new ArgumentException("Map not found!");

            map.Title = model.Title;
            map.DescriptionHTML = model.DescriptionHTML;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<LinkModel> GetLinkModel(int id)
        {
            var ml = await _context.MapLinks.SingleOrDefaultAsync(m => m.Id == id);
            if (ml == null) throw new ArgumentException("Link Map not found!");

            return new LinkModel()
            {
                Id = ml.Id,
                Description = ml.Description,
                DestinationId = ml.DestinationId,
                DestinationType = (BussinessLogic.Models.Blog.LinkDestination)(int)ml.DestinationType,
                ImageSource = ml.ImageSource,
                MapId = ml.MapId,
                OrderWeight = ml.OrderWeight,
                Title = ml.Title
            };
        }

        public async Task<LinkModel> AddMapLinkAsync(LinkModel model)
        {
            var mapLink = new Database.Entities.MapLink()
            {
                Title = model.Title,
                Description = model.Description,
                DestinationId = model.DestinationId,
                DestinationType = (Database.Entities.MapLinkDestinationType)(int)model.DestinationType,
                MapId = model.MapId,
                ImageSource = model.ImageSource,
                OrderWeight = model.OrderWeight
            };

            await _context.MapLinks.AddAsync(mapLink);
            await _context.SaveChangesAsync();

            model.Id = mapLink.Id;
            return model;
        }

        public async Task<LinkModel> EditMapLinkAsync(LinkModel model)
        {
            var mapLink = await _context.MapLinks.SingleOrDefaultAsync(m => m.Id == model.Id);
            if (mapLink == null) throw new ArgumentException("MapLink not found!");

            mapLink.Title = model.Title;
            mapLink.Description = model.Description;
            mapLink.DestinationId = model.DestinationId;
            mapLink.DestinationType = (Database.Entities.MapLinkDestinationType)(int)model.DestinationType;
            mapLink.MapId = model.MapId;
            mapLink.ImageSource = model.ImageSource;
            mapLink.OrderWeight = model.OrderWeight;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task DeleteMapLinkAsync(int id)
        {
            var mapLink = await _context.MapLinks.SingleOrDefaultAsync(m => m.Id == id);
            if (mapLink == null) throw new ArgumentException("MapLink not found!");

            _context.Remove(mapLink);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
