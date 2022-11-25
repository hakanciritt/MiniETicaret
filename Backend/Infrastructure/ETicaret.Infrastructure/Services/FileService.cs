using ETicaret.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaret.Infrastructure.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formCollection)
        {
            string uploadPath = Path.Combine(_environment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<bool> results = new();
            List<(string fileName, string path)> list = new();
            foreach (IFormFile file in formCollection)
            {
                string fileNewName = await FileRenameAsync(file.FileName);
                bool result = await CopyFileAsync(Path.Combine(uploadPath, "\\", fileNewName), file);
                results.Add(result);
                list.Add((fileNewName, $"{path}\\{fileNewName}"));
            }

            if (results.TrueForAll(d => d.Equals(true)))//hepsi true diye mi baktım içlerinde false var ise
            {
                return list;
            }

            //todo: eğer ki yukarıdaki if geçerli değilse dosyaların sunucuda yüklenirken hata alındğına daire exception oluşturulup fırlatılması gerekir.
            return null;
        }
        private async Task<string> FileRenameAsync(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = NameOperation.CharacterRegulatory(oldName) + "-" + DateTime.Now.Ticks + extension;
            return newFileName;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
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
    }
}
