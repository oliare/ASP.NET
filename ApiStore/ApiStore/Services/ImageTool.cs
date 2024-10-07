using ApiStore.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

public class ImageTool(IConfiguration configuration) : IImageTool
{
    public bool Delete(string fileName)
    {
        try
        {
            var dir = configuration["ImagesDir"];
            var sizes = configuration["ImagesSizes"].Split(",")
                .Select(int.Parse);
            foreach (var size in sizes)
            {
                string dirSave = Path.Combine(Directory.GetCurrentDirectory(),
                    dir, $"{size}_{fileName}");

                if (File.Exists(dirSave)) File.Delete(dirSave);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> Save(IFormFile image)
    {
        string imageName = String.Empty;
        var dir = configuration["ImagesDir"];

        using (MemoryStream ms = new())
        {
            await image.CopyToAsync(ms);
            var bytes = ms.ToArray();
            imageName = SaveByImageSize(bytes);
        }

        return imageName;
    }

    public async Task<string> SaveImageByUrl(string url)
    {
        string fname = String.Empty;
        using (var client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                fname = SaveByImageSize(imageBytes);
            }
        }
        return fname;
    }

    private string SaveByImageSize(byte[] bytes)
    {
        string imageName = Guid.NewGuid().ToString() + ".webp";

        var dir = configuration["ImagesDir"];
        var sizes = configuration["ImagesSizes"].Split(",")
                .Select(int.Parse);

        foreach (var size in sizes)
        {
            string dirSave = Path.Combine(Directory.GetCurrentDirectory(), dir, $"{size}_{imageName}");
            using (var imageLoad = Image.Load(bytes))
            {
                imageLoad.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(size, size),
                    Mode = ResizeMode.Max
                }));

                imageLoad.Save(dirSave, new WebpEncoder());
            }
        }
        return imageName;
    }

}