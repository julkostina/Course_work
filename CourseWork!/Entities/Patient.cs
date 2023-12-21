using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CourseWork_.Entities
{
    [Serializable]
    [KnownType(typeof(Patient))]
    public class Patient : Human
    {
        public Patient() { }
        public Patient(string name, string surname, string specialField) : base(name, surname, specialField)
        {
        }
        public static string ViewElectronicCard(ref ArrayList iOfPatients,ref ArrayList list,ref ArrayList schedule)
        {
            string res = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Patient)
                {
                    Patient patient = (Patient)list[i];
                    for (int j = 0; j < schedule.Count; j++)
                    {
                        Schedule localSchedule = (Schedule)schedule[i];
                        if (localSchedule.patient == patient)
                        {
                            res=$"{i}.{patient.Name} {patient.Surname} and has doctor: {localSchedule.doctor}";
                        }
                        else
                        {
                            res= $"{i}.{patient.Name} {patient.Surname} and does not have a doctor";
                        }
                    }
                    iOfPatients.Add(i);
                }
            }
            return res;
        }
        public static Patient AddPatient(string name,string surname,string diagnosis, ref ArrayList list,string FilePath)
        {
            list.Add(new Patient(name, surname, diagnosis));
            EntityContext.WriteFile(list, FilePath);
            return (Patient)list[list.Count-1];
        }
        public static Patient DeletePatient(int num,ref ArrayList list, string FilePath)
        {
            Patient deleted = (Patient) list[num];
            list.RemoveAt(num);
            EntityContext.ClearFile(FilePath);
            EntityContext.WriteFile(list, FilePath);
            return deleted;
        }
        public override string ToString()
        {
            return $"{Name} {Surname} has {SpecialField}";
        }
    }
}
