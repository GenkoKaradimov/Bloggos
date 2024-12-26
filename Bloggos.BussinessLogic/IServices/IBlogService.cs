using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggos.BussinessLogic.Models.Blog;
using Bloggos.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bloggos.BussinessLogic.IServices
{
    public interface IBlogService
    {
        #region Articles

        Task<List<ArticleModel>> AllArticles();

        Task<ArticleModel> GetArticleAsync(int id);

        Task<ArticleModel> AddArticleAsync(ArticleModel model);

        Task<ArticleModel> EditArticleAsync(ArticleModel model);

        #endregion

        #region Map`s pages

        Task<MapPagesModel> GetMapPagesAsync();

        Task<MapPageModel> GetMapPageAsync(int id);

        Task<MapPageModel> AddMapPageAsync(MapPageModel model);

        Task<MapPageModel> EditMapPageAsync(MapPageModel model);

        Task<LinkModel> GetLinkModel(int id);

        Task<LinkModel> AddMapLinkAsync(LinkModel model);

        Task<LinkModel> EditMapLinkAsync(LinkModel model);

        Task DeleteMapLinkAsync(int id);

        #endregion
    }
}
