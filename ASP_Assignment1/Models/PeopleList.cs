using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Assignment1.Models
{
    public class PeopleList
    {
        public string Filter { get; set; }
        public IEnumerable<Person> People => Filtered(Filter);
        static public List<Person> List { get; set; } = Pregen;

        static public IEnumerable<Person> Filtered(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return List;
            var people = List;
            return people.Where(p => TestName(p, filter) || TestCity(p, filter));
        }

        static bool TestName(Person person, string filter) => person.Name != null && person.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        static bool TestCity(Person person, string filter) => person.City != null && person.City.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        
        static List<Person> Pregen => new List<Person>
            {
                new Person{Name="Ada", Age=10, City="Stockholm"},
                new Person{Name="Steve", Age=14, City="Stockholm"},
                new Person{Name="Göran", Age=70, City="Stockholm"},
                new Person{Name="Sputnik", Age=55, City="Moscov"},
                new Person{Name="Anna", Age=10, City="Kalmar"},
                new Person{Name="Sleipnir", Age=1780, City="Asgård"},
                new Person{Name="Cedric", Age=55, City="London"},
                new Person{Name="Danny", Age=24, City="London"},
                new Person{Name="Emil", Age=16, City="Lönneberga"},
                new Person{Name="Fenrick", Age=38, City="London"},
                new Person{Name="Quasemodo", Age=99, City="Paris"},
                new Person{Name="William", Age=7, City="London"},
                new Person{Name="Esmeralda", Age=1, City="Stockholm"},
                new Person{Name="Rut", Age=25, City="Kalmar"},
                new Person{Name="Tove", Age=46, City="Stockholm"},
                new Person{Name="Yngve", Age=64, City="Kalmar"},
                new Person{Name="Ulla", Age=25, City="Västerås"},
                new Person{Name="Uhla", Age=33, City="Westeros"},
                new Person{Name="Missingno", Age=int.MinValue, City=null},
            };

    }
}
