using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyDuAnNoiBo.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;

namespace quanLyDuAnNoiBo.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    public class UploadController : quanLyDuAnNoiBoController
    {
        private readonly IBlobContainer _blobContainer;

        public UploadController(
            IBlobContainer blobContainer)
        {
            _blobContainer = blobContainer;
        }

        [HttpPost]
        public async Task<string> UploadFile()
        {
            // Nếu không có file
            if (Request.Form.Files.Count <= 0 || Request.Form.Files == null)
            {
                throw new UserFriendlyException("File Not Found");
            }

            int allowedFileSizeInConfigAsync = 25;
            string allowedExtensionsInConfig = "JPG, JPEG, PNG, PDF, DOC, DOCX, XLSX";
            List<string> allowedExtensions = allowedExtensionsInConfig.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            var file = Request.Form.Files[0];
            string filePath = "";
            var fileSize = file.Length / 1024 / 1024;
            string fileExtension = System.IO.Path.GetExtension(file.FileName).Replace(".", string.Empty).ToUpper();
            if (allowedExtensions.Any(e => e.Trim() == fileExtension))
            {
                if (fileSize > allowedFileSizeInConfigAsync)
                {
                    throw new FileWrongFormatException().WithData("MaxSize", allowedFileSizeInConfigAsync).WithData("FileFormat", allowedExtensionsInConfig);
                }
                filePath = $"Upload{System.IO.Path.DirectorySeparatorChar}Gift{System.IO.Path.DirectorySeparatorChar}Images";
            }
            else
            {
                throw new FileWrongFormatException().WithData("MaxSize", allowedFileSizeInConfigAsync).WithData("FileFormat", allowedExtensionsInConfig);
            }

            string rs = string.Empty;
            rs = await SaveFile(filePath, file);
            return await Task.FromResult(rs);
        }

        private async Task<string> SaveFile(string FolderPath, IFormFile ImportFile)
        {
            byte[] fileBytes;
            using (System.IO.Stream stream = ImportFile.OpenReadStream())
            {
                fileBytes = stream.GetAllBytes();
            }

            var extensionOfFile = System.IO.Path.GetExtension(ImportFile.FileName);

            string uploadFileName = string.Format("{0:yyyyMMdd_hhmmss}_", DateTime.Now) + ImportFile.Length.ToString() + extensionOfFile;

            await _blobContainer.SaveAsync(System.IO.Path.Combine(FolderPath, uploadFileName).Replace('\\', '/'), fileBytes);

            var rs = System.IO.Path.Combine(FolderPath, uploadFileName).Replace('\\', '/');

            return await Task.FromResult(rs);
        }
    }
}
