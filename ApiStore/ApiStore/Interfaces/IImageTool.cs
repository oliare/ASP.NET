namespace ApiStore.Interfaces
{
    public interface IImageTool
    {
        Task<string> Save(IFormFile image);
        bool Delete(string file);
    }
}