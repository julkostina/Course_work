using DAL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace PL.Serialization
{
    
    public class XML : DataProvider
    {
        public static void Write(string file, ArrayList data)
        {
            using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ArrayList) );
                formatter.Serialize(fileStream, data);
            }
        }
        public static ArrayList Read(string file)
        {
            ArrayList deserializedObj;
            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ArrayList));
                deserializedObj = (ArrayList)xmlSerializer.Deserialize(fileStream);
            }
            return deserializedObj;
        }
    }
}
