using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CloudinaryService
{
    public class CloudinaryService(Cloudinary _cloudinary) : ICloudinaryService
    {
        public async Task<string> Upload(IFormFile file)
        {
            if (file == null)
            {
                return "File not found";
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParam = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Folder = "EposhBooking",
                    PublicId = Guid.NewGuid().ToString(),
                    Transformation = new Transformation().Quality("auto:low")
                                                         .FetchFormat("webp")
                                                         .Width(1024)
                                                         .Crop("limit")
                };


                var uploadResult = await _cloudinary.UploadAsync(uploadParam);
                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    throw new Exception("Failed to upload image to Cloudinary.");
                }
            }
        }
    }
}
