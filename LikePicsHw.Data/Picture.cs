using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LikePicsHw.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int LikesCount { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class Repository
    {
        private readonly string _connection;

        public Repository(string connection)
        {
            _connection = connection;
        }

        public List<Image> GetAllPics()
        {
            using var context = new PicDataContext(_connection);
            return context.Pictures.ToList();
        }

        public void Upload(Image image)
        {
            using var context = new PicDataContext(_connection);
            context.Pictures.Add(image);
            context.SaveChanges();
        }

        public Image GetImageById(int id)
        {
            using var context = new PicDataContext(_connection);
            return context.Pictures.FirstOrDefault(p => p.Id == id);
        }

        public void AddLike(int id)
        {
            using var context = new PicDataContext(_connection);
            var image = context.Pictures.FirstOrDefault(p => p.Id == id);
            image.LikesCount++;
            context.SaveChanges();
        }
    }
}
