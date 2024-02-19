using Microsoft.Graph;
using Microsoft.VisualBasic.FileIO;
using MSGraph.SendEmail.Model;

namespace MSGraph.SendEmail.File_Attachment
{
    public class FormFileUpload
    {
        public async Task<List<FileAttachment>> UploadAttachment(List<IFormFile> files)
        {
            List<FileAttachment> filesuploaded = new List<FileAttachment>();
            foreach(var formfile in files)
            {
                if(formfile.Length > 0)
                {
                    var basePath = Path.Combine(System.IO.Directory.GetCurrentDirectory() + "\\Files\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) System.IO.Directory.CreateDirectory(basePath);
                    var fileName = Path.GetFileName(formfile.FileName);
                    var filePath = Path.Combine(basePath, formfile.FileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formfile.CopyToAsync(stream);
                        }
                        byte[] contentBytes = System.IO.File.ReadAllBytes(filePath);
                        // byte[] contentBytes = System.IO.File.ReadAllBytes(Path.GetFullPath(file.FileName));
                        var filetype = formfile.ContentType;

                        filesuploaded.Add( new FileAttachment
                        {
                            Name = fileName,
                            ContentBytes = contentBytes,
                            ODataType = "#microsoft.graph.fileAttachment",
                            ContentType = filetype

                        });
                        System.IO.File.Delete(filePath);
                    }                    
                }
            }
            return filesuploaded;
        }
    }
}
