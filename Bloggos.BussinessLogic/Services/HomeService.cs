using Bloggos.BussinessLogic.IServices;
using Bloggos.BussinessLogic.Models.Blog;
using Bloggos.BussinessLogic.Models.Home;
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
    public class HomeService : IHomeService
    {
        private readonly BloggosDbContext _context;

        public HomeService(BloggosDbContext context)
        {
            _context = context;
        }

        #region Images

        public async Task<ImagesModel> GetImagesAsync(int? count, int? page)
        {
            count ??= 24;
            page ??= 1;

            if (count <= 0 || page <= 0)
                throw new ArgumentException("Count and page must be positive integers.");

            var images = await _context.Images
                .OrderBy(x => x.Id)
                .Skip(count.Value * (page.Value - 1))
                .Take(count.Value)
                .Select(x => x.Id)
                .ToListAsync();

            return new ImagesModel
            {
                ImagesIds = images
            };
        }

        public async Task<ImageModel> GetImageAsync(int id)
        {
            var image = await _context.Images.SingleOrDefaultAsync(x => x.Id == id);
            if (image == null) throw new ArgumentException("Image is not found!");

            return new ImageModel()
            {
                Id = id,
                ImageData = image.ImageData,
                ContentType = image.ContentType,
            };
        }

        public async Task<ImageModel> AddImageAsync(ImageModel model)
        {
            var image = new Database.Entities.Image()
            {
                ContentType = model.ContentType,
                ImageData = model.ImageData
            };

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            model.Id = model.Id;
            return model;
        }

        public async Task<ImageModel> EditImageAsync(ImageModel model)
        {
            var image = await _context.Images.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (image == null) throw new ArgumentException("Image is not found!");

            image.ImageData = model.ImageData;
            image.ContentType = model.ContentType;
            await _context.SaveChangesAsync();

            return model;
        }

        #endregion
    }
}
