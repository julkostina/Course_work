using PL.Serialization;
using System.Collections;
using System.Xml.Serialization;

namespace DLL
{
    public class EntityContext
    {
        public static string CreateFile(string name)
        {
            return AppDomain.CurrentDomain.BaseDirectory  + name;
        }
        public static void WriteFile(ArrayList data,string path)
        {
            if (path.Contains(".bin"))
            {
                Binary.Write(path, data);
            }
            if (path.Contains(".dat"))
            {
                Custom.Write(path, data);
            }
            if (path.Contains(".json"))
            {
                JSON.Write(path, data);
            }
            if (path.Contains(".xml"))
            {
                XML.Write(path, data);
            }
        }
        public static string ReadFile(string path)
        {
            using (StreamReader streamReader = new(path, System.Text.Encoding.Default))
            {
                return streamReader.ReadToEnd();
            }
        }
        public static void ClearFile(string path)
        {
            using(FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                fileStream.SetLength(0);
            }
        }
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
