using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseWork_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DLL;
using System.Xml.Linq;

namespace CourseWork_.Entities.Tests
{
    [TestClass()]
    public class PatientTests
    {
        [TestMethod()]
        public void ViewElectronicCardTest()
        {
            ArrayList list = new ArrayList();
            ArrayList schedule = new ArrayList();
            string SchedulePath = EntityContext.CreateFile("file.json");
            ArrayList iOfPatients = new ArrayList() { 0 };
            Schedule.MakeAppointment("14:00", "12.12.2023", new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov", "Flu"), ref schedule, SchedulePath);
            string actualData = Patient.ViewElectronicCard(ref iOfPatients, ref list, ref schedule);
            Assert.IsNotNull(schedule);
        }

        [TestMethod()]
        public void AddPatientTest()
        {
            ArrayList list = new ArrayList();
            string FilePath = EntityContext.CreateFile("file.json");
            Patient actual = Patient.AddPatient("Jane", "Watson", "Cold", ref list, FilePath);
            Patient expected = new Patient("Jane", "Watson", "Cold");
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Surname, actual.Surname);
            Assert.AreEqual(expected.SpecialField, actual.SpecialField);

        }

        [TestMethod()]
        public void DeletePatientTest()
        {
            ArrayList list = new ArrayList();
            string FilePath = EntityContext.CreateFile("file.json");
            Patient expected = Patient.AddPatient("Jane", "Watson", "Cold", ref list, FilePath);
            Patient actual = Patient.DeletePatient(0, ref list, FilePath);
            Assert.AreEqual(expected,actual);
            Assert.IsTrue(list.Count == 0);
        }
    }
}