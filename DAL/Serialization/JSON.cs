using DAL.Interface;
using System.Collections;
using System.Runtime.Serialization.Json;


namespace PL.Serialization
{
    internal class JSON : DataProvider
    {
        public static void Write(string file, ArrayList data)
        {
            
            using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(data.GetType());
                dataContractJsonSerializer.WriteObject(fileStream, data);
            }
        }

      
        public static ArrayList Read(string file)
        {
            ArrayList deserializedObj;
            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(ArrayList));
                deserializedObj = (ArrayList)dataContractJsonSerializer.ReadObject(fileStream);
            }
            return deserializedObj;
        }

    }
}
