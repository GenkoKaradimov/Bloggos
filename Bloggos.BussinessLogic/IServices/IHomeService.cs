using Bloggos.BussinessLogic.Models.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.IServices
{
    public interface IHomeService
    {
        #region Images

        Task<ImagesModel> GetImagesAsync(int? count, int? page);

        Task<ImageModel> GetImageAsync(int id);

        Task<ImageModel> AddImageAsync(ImageModel model);

        Task<ImageModel> EditImageAsync(ImageModel model);

        #endregion
    }
}
