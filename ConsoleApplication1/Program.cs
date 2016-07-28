using RenamePhotos.Code;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System;

namespace RenamePhotos
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Code.File.GetFiles();

            var photos = new List<Photo>();

            foreach(var filePath in files)
            {
                var photo = new Photo(filePath);
                photo.SetDateTaken();
                photo.SetCreationDate();
                photo.SetCameraMaker();
                photo.SetCameraModel();
                photos.Add(photo);
            }

            var dirPath = $"Переименованные фото_{DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss")}";
            var dir = Directory.CreateDirectory(dirPath);

            foreach(var photo in photos)
            {
                System.IO.File.Copy(photo.FilePath, $"{dir.FullName}\\{photo.GetFileName(photos)}", true);

            }
        }
    }
}
