namespace ApiStore.Interfaces
{
    public interface IImageTool
    {
        Task<string> Save(IFormFile image);
        Task<string> SaveImageByUrl(string url);
        bool Delete(string fileName);
    }
}