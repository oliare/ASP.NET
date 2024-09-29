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
        string imageName = Guid.NewGuid().ToString() + ".webp";
        var dir = configuration["ImagesDir"];

        using (MemoryStream ms = new())
        {
            await image.CopyToAsync(ms);
            var bytes = ms.ToArray();
            await SpecifyImageSize(bytes, imageName);
        }

        return imageName;
    }

    public async Task<string> SaveImageByUrl(string url)
    {
        using (var httpClient = new HttpClient())
        {
            var bytes = await httpClient.GetByteArrayAsync(url);
            string fname = Guid.NewGuid().ToString() + ".webp";
            var dir = configuration["ImagesDir"];

            string dirSave = Path.Combine(Directory.GetCurrentDirectory(), dir, fname);

            using (var image = Image.Load(bytes))
            {
                image.Save(dirSave, new WebpEncoder());
            }

            return fname;
        }
    }

    private async Task SpecifyImageSize(byte[] bytes, string imageName)
    {
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

                await Task.Run(() => imageLoad.Save(dirSave, new WebpEncoder()));
            }
        }
    }

}