using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggos.BussinessLogic.Models.Blog;
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

        Task<MapPageModel> GetMapPageAsync(int id);
    }
}
