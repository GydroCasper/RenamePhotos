using System;
using System.Linq;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Shell;
using System.Collections.Generic;

namespace RenamePhotos.Code
{
    internal class Photo
    {
        private string _filePath;
        private DateTime? _dateTaken;
        private DateTime _creationTime;
        private string _cameraMaker;
        private string _cameraModel;
        private Image _image;
        private ShellObject _shellFile;
        private string _fileExtension;

        private DateTime Date => _dateTaken ?? _creationTime;

        private string FileExtension => _fileExtension;
        internal string FilePath => _filePath;
        private string DateString => Date.ToString("yyyy-MM-dd HH-mm");
        private string DateStringFull => Date.ToString("yyyy-MM-dd HH-mm-ss");

        internal Photo(string filePath)
        {
            _image = Image.FromFile(filePath);
            _filePath = filePath;
            _shellFile = ShellFile.FromParsingName(filePath);
            _fileExtension = _shellFile.Properties.System.FileExtension.Value;
        }

        private Photo()
        { }

        internal string GetFileName(List<Photo> photos, int index)
        {
            var isPreviousPhotoHasDifferentDateString = index == 0 || photos[index - 1].DateString != DateString;
            var isNexPhotoHasDifferentDateString = index == photos.Count - 1 || photos[index + 1].DateString != DateString;

            //var datePart = photos.Count(x => x.DateString == DateString) == 1 ? DateString : DateStringFull;
            var datePart = isPreviousPhotoHasDifferentDateString && isNexPhotoHasDifferentDateString ? DateString : DateStringFull;

            var cameraPart = _cameraMaker != _cameraModel ? $"{_cameraMaker} {_cameraModel}" : _cameraMaker;
            return $"{datePart} ({cameraPart}){_fileExtension}";
        }

        internal void SetDateTaken()
        {
            _dateTaken = _shellFile.Properties.System.Photo.DateTaken.Value;

            //var date = GetMetadataProperty(0x132);

            //if (!string.IsNullOrEmpty(date))
            //{
            //    date = date.Replace('-', ':');
            //    DateTaken =  DateTime.ParseExact(date, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
            //}
        }

        internal void SetCreationDate()
        {
            _creationTime = System.IO.File.GetCreationTime(_filePath);
        }

        internal void SetCameraMaker()
        {
            _cameraMaker = _shellFile.Properties.System.Photo.CameraManufacturer.Value;

            //CameraMaker = GetMetadataProperty(0x010F);
        }

        internal void SetCameraModel()
        {
            _cameraModel = _shellFile.Properties.System.Photo.CameraModel.Value;
            //CameraModel = GetMetadataProperty(0x0110);
        }

        internal static List<Photo> Sort(List<Photo> photos)
        {
            return photos.OrderBy(x => x.Date).ToList();
        }

        //private string GetMetadataProperty(int propertyId)
        //{
        //    if (_image.PropertyIdList.ToList().Contains(propertyId))
        //    {
        //        var propertyValue = _image.GetPropertyItem(propertyId);

        //        var encoding = new ASCIIEncoding();
        //        return encoding.GetString(propertyValue.Value, 0, propertyValue.Len - 1);
        //    }

        //    return null;
        //}
    }
}
