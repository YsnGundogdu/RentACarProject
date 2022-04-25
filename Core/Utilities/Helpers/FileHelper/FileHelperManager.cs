﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            return Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);

                string fileExtension = Path.GetExtension(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string filePath = guid + fileExtension;

                FileStream fileStream = File.Create(root + filePath);
                file.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();

                return filePath;
            }

            return null;
        }
    }
}
