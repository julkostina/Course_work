using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CourseWork_.Entities
{
    [Serializable]
    public class Human 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SpecialField { get; set; }
        public Human(string name, string surname, string specialField)
        {
            Name = name;
            Surname = surname;
            SpecialField = specialField;
        }
        public Human() { }
    }
}
