using CourseWork_.Entities;
using DLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Numerics;

namespace CourseWork_.Entities.Tests
{
    [TestClass()]
    public class ScheduleTests
    {
        [TestMethod()]
        public void DoctorsSchedule()
        {
            ArrayList schedule = new ArrayList();
            schedule.Add(new Schedule("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu")));
            string expected = "Doctor:Jane Earl(Surgeon) has an appointment with: Victor Abramov ";
            string actual = Schedule.DoctorsSchedule("Jane", "Earl", "14:00", schedule);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void WrongDoctorsSchedule()
        {
            ArrayList schedule = new ArrayList();
            schedule.Add(new Schedule("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu")));
            string expected = null;
            string actual = Schedule.DoctorsSchedule("Jane", "Earl", "12:00", schedule);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void FindPatient()
        {
            ArrayList schedule = new ArrayList();
            schedule.Add(new Schedule("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu")));
            string expected = "14:00 12.12.2023\npatient: Victor Abramov with diagnosis Flu has an appointment with doctor:Jane Earl(Surgeon)";
            string actual = Schedule.FindPatient("Victor", "Abramov", schedule);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void NotFoundPatient()
        {
            ArrayList schedule = new ArrayList();
            schedule.Add(new Schedule("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu")));
            string expected = null;
            string actual = Schedule.FindPatient("Victor", "Grey", schedule);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void FindDoctor()
        {
            ArrayList schedule = new ArrayList();
            schedule.Add(new Schedule("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu")));
            string expected = "14:00 12.12.2023\npatient: Victor Abramov with diagnosis Flu has an appointment with doctor:Jane Earl(Surgeon)";
            string actual = Schedule.FindDoctor("Jane", "Earl", schedule);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void NotFoundDoctor()
        {
            ArrayList schedule = new ArrayList();
            schedule.Add(new Schedule("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu")));
            string expected = null;
            string actual = Schedule.FindDoctor("Jane", "Abramov", schedule);
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void AppointmentNotEmptyListTest()
        {
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            Assert.IsNotNull(schedule);
        }
        [TestMethod()]
        public void AppointmentNotEmptyFileTest()
        {
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            Assert.IsNotNull(EntityContext.ReadFile(SchedulePath));
        }

        [TestMethod()]
        public void ScheduleEmptyListTest()
        {
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            Schedule.DeleteSchedule(ref schedule, 0, SchedulePath);
            Assert.IsTrue(schedule.Count==0);
        }
        [TestMethod()]
        public void ScheduleEmptyFileTest()
        {
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            Schedule.DeleteSchedule(ref schedule, 0, SchedulePath);
            Assert.AreEqual(EntityContext.ReadFile(SchedulePath),"");
        }

        [TestMethod()]
        public void ChangeTimeTest()
        {
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            string expectedTime = "13:00";
            Schedule currentSchedule = (Schedule)schedule[0];
            Schedule.ChangeTime(expectedTime, currentSchedule, schedule, SchedulePath);
            Schedule changedSchedule = (Schedule)schedule[0];
            Assert.AreEqual(expectedTime, changedSchedule.Time);
        }

        [TestMethod()]
        public void ChangeDateTest()
        {
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            string expectedDate = "13.12.2023";
            Schedule currentSchedule = (Schedule)schedule[0];
            Schedule.ChangeDate(expectedDate, currentSchedule, schedule, SchedulePath);
            Schedule changedSchedule = (Schedule)schedule[0];
            Assert.AreEqual(expectedDate, changedSchedule.Date);
        }
    }
}