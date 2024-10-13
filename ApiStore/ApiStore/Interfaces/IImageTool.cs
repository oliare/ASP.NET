namespace ApiStore.Interfaces
{
    public interface IImageTool
    {
        Task<string> Save(IFormFile image);
        Task<string> SaveImageByUrl(string url);
        string SaveImageFromBase64(string image);
        bool Delete(string fileName);
    }
}