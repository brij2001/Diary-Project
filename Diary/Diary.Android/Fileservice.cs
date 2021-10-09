using System.IO;
using static Diary.Views.NoteEntryPage;

namespace Diary.Droid
{
    public class FileService
    {
        public void SavePicture(string photoName,string documentsPath, Stream data)
        {
            
            documentsPath = Path.Combine(documentsPath, "imagesFolder");
            Directory.CreateDirectory(documentsPath);

            string filePath = Path.Combine(documentsPath, photoName);

            byte[] bArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(bArray, 0, (int)data.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }
    }
}