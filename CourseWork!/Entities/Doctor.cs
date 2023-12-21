using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;
using System.Xml.Serialization;
using DLL;
using System.Xml.Linq;

namespace CourseWork_.Entities
{
    [Serializable]
    [KnownType(typeof(Doctor))]
    public class Doctor : Human
    {
        public Doctor() { }
        public Doctor(string name, string surname, string specialField) : base(name, surname, specialField)
        {
        }
        public static Doctor AddedDoctor(string name, string surname, string speciality, ref ArrayList list, string FilePath)
        {
            list.Add(new Doctor(name, surname, speciality));
            EntityContext.WriteFile(list, FilePath);
            return (Doctor)list[list.Count - 1];
        }
        public static Doctor DeleteDoctor(int num, ref ArrayList list, string FilePath)
        {
            Doctor deleted = (Doctor)list[num];
            list.RemoveAt(num);
            EntityContext.ClearFile(FilePath);
            EntityContext.WriteFile(list, FilePath);
            return deleted;
        }
        public  override string ToString()
        {
            return $"{Name} {Surname} is {SpecialField}";
        }
    }
}
