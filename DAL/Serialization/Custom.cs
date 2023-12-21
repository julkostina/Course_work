using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.IO;
using DAL.Interface;
using System.Collections;

namespace PL.Serialization
{
    public class Custom : DataProvider
    {
        public static void Write(string file, ArrayList data)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fileStream, data);
            }

        }
        public static ArrayList Read(string file)
        {
            IFormatter formatter = new BinaryFormatter();
            ArrayList deserializedObj;
            using (Stream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                deserializedObj = (ArrayList)formatter.Deserialize(fileStream);
            }
            return deserializedObj;
        }


    }
}

