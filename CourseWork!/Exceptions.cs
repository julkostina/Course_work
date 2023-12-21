using System.Text.RegularExpressions;


namespace Bussiness_layer
{
    public class Exceptions
    {
        public Exceptions() { }
        public static Regex validInput = new (@"^[A-Z]{1}[a-z]{2,20}$");
        public static Regex validFile = new Regex(@"[A-Za-z]\d");
        public static Regex vaidDate = new Regex(@"[1-31]{1,2}\.[1-12]{1,2}\.202[3-9]{1}");
        public static Regex validTime = new Regex(@"\d{1,2}:\d{2}");
    }
}
