namespace final_project_server.Uploads
{
    public static class UploadHandler
    {
        public static string Upload(IFormFile file)
        {
            List<string> validExtensions = new List<string>() { ".png", ".jpeg", ".gif" };
            string extension = Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(extension))
            {
                return "Invalid file type!";
            }
            long fileSize = file.Length;
            if (fileSize > 5*1024*1024)
            {
                return "File must be less then 5MB!";
            }
            string fileName = file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(),"Uploads\\Images");
            using FileStream stream = new FileStream(Path.Combine(path,fileName),FileMode.Create);
            file.CopyTo(stream);
            return (Path.Combine(path, fileName).ToString());
        }
    }
}
