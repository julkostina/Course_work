using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using DAL.Interface;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Collections;

namespace PL.Serialization
{
    public class Binary : DataProvider
    {
        public static void Write(string file, ArrayList data)
        {

            using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormarter = new BinaryFormatter();
                binaryFormarter.Serialize(fileStream, data);
            }
        }
        public static ArrayList Read(string file)
        {
            ArrayList deserializedObject;
            using (FileStream fileStream = new(file, FileMode.Open))
            {
                BinaryFormatter binaryFormater = new();
                deserializedObject = (ArrayList)binaryFormater.Deserialize(fileStream);
            }
            return deserializedObject;
        }
    }
}
