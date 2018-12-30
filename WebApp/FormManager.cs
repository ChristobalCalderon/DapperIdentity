using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class FileManager
    {
        public async Task UploadFile(IFormFile file)
        {
            try
            {
                //1 check if the file length is greater than 0 bytes 
                if (file.Length > 0)
                {
                    string fileName = file.FileName;
                    //2 Get the extension of the file
                    string extension = Path.GetExtension(fileName);
                    //3 check the file extension as png
                    if (extension == ".png" || extension == ".jpg")
                    {
                        //4 set the path where file will be copied
                        string filePath = Path.GetFullPath(
                            Path.Combine(Directory.GetCurrentDirectory(),
                                                        "UploadedFiles"));
                        //5 copy the file to the path
                        using (var fileStream = new FileStream(
                            Path.Combine(filePath, fileName),
                                           FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        throw new Exception("File must be either .png or .JPG");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
