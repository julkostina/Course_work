using DLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace CourseWork_.Entities.Tests
{
    [TestClass()]
    public class DoctorTests
    {
        [TestMethod()]
        public void AddedDoctorTest()
        {
            ArrayList list = new ArrayList();
            string FilePath = EntityContext.CreateFile("file.json");
            Doctor actual = Doctor.AddedDoctor("Jane", "Watson", "Gynecologist", ref list, FilePath);
            Doctor expected = new Doctor("Jane", "Watson", "Gynecologist");
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Surname, actual.Surname);
            Assert.AreEqual(expected.SpecialField, actual.SpecialField);
        }
        [TestMethod()]
        public void DeleteDoctorTest()
        {
            ArrayList list = new ArrayList();
            string FilePath = EntityContext.CreateFile("file.json");
            Doctor expected = Doctor.AddedDoctor("Jane", "Watson", "Gynecologist", ref list, FilePath);
            Doctor actual = Doctor.DeleteDoctor(0, ref list, FilePath);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(list.Count == 0);
        }
    }
}