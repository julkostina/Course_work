
namespace DAL.Interface
{
    public interface DataProvider
    {
        static void Write(string file, object data) { }
        static object Read(string file) { return default(object); }
    }
}

