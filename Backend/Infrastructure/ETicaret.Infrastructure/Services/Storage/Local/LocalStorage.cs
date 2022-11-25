using ETicaret.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaret.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _environment;

        public LocalStorage(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_environment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> list = new();
            foreach (IFormFile file in files)
            {
                bool result = await CopyFileAsync(Path.Combine(uploadPath, "\\", file.FileName), file);

                list.Add((file.FileName, $"{path}\\{file.FileName}"));
            }

            //todo: eğer ki yukarıdaki if geçerli değilse dosyaların sunucuda yüklenirken hata alındğına daire exception oluşturulup fırlatılması gerekir.
            return null;
        }
        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream stream =
                    new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 2024);
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
                return true;
            }
            catch (Exception e)
            {
                //todo:log mekanizması yapılacak.
                throw e;
            }
        }

        public async Task DeleteAsync(string path, string fileName)
        {
            File.Delete($"{path}\\{fileName}");
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(c => c.Name).ToList();

        }

        public bool HasFile(string path, string fileName)
        {
            return File.Exists($"{path}\\{fileName}");
        }
    }
}
