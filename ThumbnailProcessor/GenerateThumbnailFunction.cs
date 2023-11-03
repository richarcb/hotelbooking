using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace ThumbnailProcessor
{
    public static class GenerateThumbnailFunction
    {
        [FunctionName("ThumbnailProcessor")]
        public static void Run(
            [BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")] Stream imageStream,
            [Blob("thumbnails/{name}", FileAccess.Write)] Stream thumbnailStream, 
            string name, ILogger log)
        {
            log.LogInformation("C# Blob trigger function processed a request.");
            var image = Image.Load(imageStream);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(100, 100),
                Mode = ResizeMode.Max
            }));

            image.Save(thumbnailStream, new JpegEncoder());
        }
    }
}
