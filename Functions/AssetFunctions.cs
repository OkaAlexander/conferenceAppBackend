using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using server.Models;
namespace server.Functions
{
    public class AssetFunctions
    {
        public string HandleUploadedFile(ParticipantModel model, IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                string basePath = webHostEnvironment.ContentRootPath;
                string filePath = string.Format("{0}.JPG", model.phone);
                string basepath = string.Format("{0}/Assets/", basePath);
                string imgPath = basepath + filePath;
                using (FileStream fileStream = File.Create(imgPath))
                {
                    model.file.CopyTo(fileStream);
                    fileStream.Flush();
                    return string.Format("{0}/{1}", "assets", filePath);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string HandleGuestFile(GuestModel model, IWebHostEnvironment webHostEnvironment)
        {
            try
            {
                Random random = new Random();
                string fname="UESG"+random.Next(77045109,88116577).ToString();
                string basePath = webHostEnvironment.ContentRootPath;
                string filePath = string.Format("{0}.JPG",fname );
                string basepath = string.Format("{0}/Assets/", basePath);
                string imgPath = basepath + filePath;
                using (FileStream fileStream = File.Create(imgPath))
                {
                    model.file.CopyTo(fileStream);
                    fileStream.Flush();
                    return string.Format("{0}/{1}", "assets", filePath);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
