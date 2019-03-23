using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Vacation360.Models.Service.Upload
{
    public class ImageUpload
    {
        public virtual string Upload(HttpPostedFileBase file, string folderName)
        {
            if (file == null)
            {
                return "";
            }

            string extension = Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid().ToString();
            string fileUrl = HttpContext.Current.Server.MapPath(folderName);

            if (!Directory.Exists(fileUrl))
            {
                Directory.CreateDirectory(fileUrl);
            }

            string pathToSave = string.Format(@"{0}\{1}{2}", fileUrl, fileName, extension);
            file.SaveAs(pathToSave);

            return string.Format("{0}{1}", fileName, extension);
        }
    }
}