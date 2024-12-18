using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggos.BussinessLogic.Models.Blog;

namespace Bloggos.BussinessLogic.IServices
{
    public interface IBlogService
    {
        Task<ArticleModel> GetArticleAsync(int id);

        Task<MapPageModel> GetMapPageAsync(int id);
    }
}
