using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CourseWork_.Entities
{
    [Serializable]
    [KnownType(typeof(Schedule))]
    public class Schedule
    {

        public Schedule() { }
        public Schedule(string time, string date, Doctor doctor, Patient patient)
        {
            Time = time;
            Date = date;
            this.doctor = doctor;
            this.patient = patient;
        }
        public string Time { get; set; }
        public string Date { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
        public static void MakeAppointment(string time, string date, Doctor doctor, Patient patient,ref ArrayList schedule, string SchedulePath)
        {
            schedule.Add(new Schedule(time, date, doctor, patient));
            EntityContext.WriteFile(schedule, SchedulePath);
        }
        public static string DoctorsSchedule(string name,string surname,string time, ArrayList scheduleList)
        {
            if (scheduleList != null)
            {
                foreach(Schedule schedule in scheduleList)
                {
                    if(name==schedule.doctor.Name&& surname == schedule.doctor.Surname && time == schedule.Time)
                    {
                        return $"Doctor:{schedule.doctor.Name} {schedule.doctor.Surname}({schedule.doctor.SpecialField}) has an appointment with: {schedule.patient.Name} {schedule.patient.Surname} ";
                    }
                }
            }
            return null;
        }
        public static string FindPatient(string name, string surname, ArrayList list)
        {
            if (list != null)
            {
                foreach (Schedule schedule in list)
                {
                    if (schedule.patient.Name == name && schedule.patient.Surname == surname)
                    {
                        return schedule.ToString();
                    }
                }
            }
            return null;
        }
        public static string FindDoctor(string name, string surname, ArrayList list)
        {
            if (list != null)
            {
                foreach (Schedule schedule in list)
                {
                    if (schedule.doctor.Name == name && schedule.doctor.Surname == surname)
                    {
                        return schedule.ToString();
                    }
                }
            }
            return null;
        }
        public static void DeleteSchedule(ref ArrayList schedule, int input, string SchedulePath)
        {
            EntityContext.ClearFile(SchedulePath);
            schedule.RemoveAt(input);
            if (schedule.Count == 0)
            {
                EntityContext.ClearFile(SchedulePath);
            }
            else
            {
                EntityContext.WriteFile(schedule, SchedulePath);
            }
        }
        public static void ChangeTime(string time,Schedule currentSchedule,ArrayList schedule, string SchedulePath)
        {
            for (int i = 0; i < schedule.Count; i++)
            {
                Schedule localSchedule = (Schedule)schedule[i];
                if (currentSchedule == localSchedule)
                {
                    schedule[i] = new Schedule(time, currentSchedule.Date, currentSchedule.doctor, currentSchedule.patient);
                    EntityContext.WriteFile(schedule, SchedulePath);
                }
            }
        }
        public static void ChangeDate(string date, Schedule currentSchedule, ArrayList schedule, string SchedulePath)
        {
            for (int i = 0; i < schedule.Count; i++)
            {
                Schedule localSchedule = (Schedule)schedule[i];
                if (currentSchedule == localSchedule)
                {
                    schedule[i] = new Schedule(currentSchedule.Time, date, currentSchedule.doctor, currentSchedule.patient);
                    EntityContext.WriteFile(schedule, SchedulePath);
                }
            }
        }

        public override string ToString()
        {
            return $"{Time} {Date}\npatient: {patient.Name} {patient.Surname} with diagnosis {patient.SpecialField} has an appointment with doctor:{doctor.Name} {doctor.Surname}({doctor.SpecialField})";
        }
    }



}
