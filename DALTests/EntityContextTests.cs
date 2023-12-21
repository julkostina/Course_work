using Microsoft.VisualStudio.TestTools.UnitTesting;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_.Entities;
using System.Collections;

namespace DLL.Tests
{
    [TestClass()]
    public class EntityContextTests
    {
        [TestMethod()]
        public void CreateFileTest()
        {
            string expected = "C:\\Users\\User\\source\\repos\\CourseWork!\\DALTests\\bin\\Debug\\net6.0\\File.bin";
            string actual = EntityContext.CreateFile("File.bin");
            Assert.AreEqual(expected,actual);
        }
        [TestMethod()]
        public void WriteFileTest()
        {
            string path = EntityContext.CreateFile("test.json");
            ArrayList list = new ArrayList() { new Schedule("12:00", "12.12.2023",new Doctor("Jane", "Earl", "Surgeon"), new Patient("Victor", "Abramov","Flu") ) };
            EntityContext.WriteFile(list,path);
            string actual = EntityContext.ReadFile(path);
            string expected = "[{\"__type\":\"Schedule:#CourseWork_.Entities\",\"<Date>k__BackingField\":\"12.12.2023\",\"<Time>k__BackingField\":\"12:00\",\"<doctor>k__BackingField\":{\"<Name>k__BackingField\":\"Jane\",\"<SpecialField>k__BackingField\":\"Surgeon\",\"<Surname>k__BackingField\":\"Earl\"},\"<patient>k__BackingField\":{\"<Name>k__BackingField\":\"Victor\",\"<SpecialField>k__BackingField\":\"Flu\",\"<Surname>k__BackingField\":\"Abramov\"}}]";
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void ReadFileTest()
        {
            string path = EntityContext.CreateFile("test.json");

            EntityContext.WriteFile(new System.Collections.ArrayList() { new Doctor("Jane", "Earl", "Surgeon") },path);
            string actual = EntityContext.ReadFile(path);
            string expected = "[{\"__type\":\"Doctor:#CourseWork_.Entities\",\"<Name>k__BackingField\":\"Jane\",\"<SpecialField>k__BackingField\":\"Surgeon\",\"<Surname>k__BackingField\":\"Earl\"}]BackingField\":\"Jane\",\"<SpecialField>k__BackingField\":\"Surgeon\",\"<Surname>k__BackingField\":\"Earl\"},\"<patient>k__BackingField\":{\"<Name>k__BackingField\":\"Victor\",\"<SpecialField>k__BackingField\":\"Flu\",\"<Surname>k__BackingField\":\"Abramov\"}}]";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void DeleteFileTest()
        {
            string path = EntityContext.CreateFile("test.xml");
            EntityContext.DeleteFile(path);

            Assert.IsTrue(!File.Exists(path));
        }
    }
}