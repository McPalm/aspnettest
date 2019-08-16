using ASP_Assignment1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Assignment1.ViewModels
{
    public class PeopleList : IEnumerable<KeyValuePair<int, Person>>
    {
        public string Filter { get; set; }
        public string Order { get; set; }
        static int index = 0;
        public Dictionary<int, Person> Dictionary { get; set; } = new Dictionary<int, Person>();
        public int Page { set; get; } = 0;

        public void Add(Person p)
        {
            Dictionary.Add(++index, p);
        }

        public void Add(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                Add(person);
            }
            // Fun fact, the first thing I do when I start a project, is importing a bunch fo my own standard extention libraries. One fo them contains a foreach extention to IEnumerables. Why is that not in a standard library?
        }  

        public IEnumerable<KeyValuePair<int,Person>> Filtered()
        {
            if (string.IsNullOrEmpty(Filter))
                return Dictionary.Skip(Page * 10).Take(10);
            return Dictionary.Where(p => TestName(p.Value, Filter) || TestCity(p.Value, Filter)).Skip(Page*10).Take(10);
        }
        public void NextPage() => Page++;
        public void PrevPage() => Page--;


        static public PeopleList GetMock()
        {
            return new PeopleList
            {
                Pregen
            };
        }

        public IEnumerable<Person> Ordered(IEnumerable<Person> people)
        {
            switch (Order.ToLower())
            {
                case "age":
                    return people.OrderBy(p => p.Age);
                case "rage":
                case "iage":
                case "dage":
                    return people.OrderByDescending(p => p.Age);
                case "name":
                    return people.OrderBy(p => p.Name);
                case "rname":
                case "iname":
                case "dname":
                    return people.OrderByDescending(p => p.Name);
                case "city":
                    return people.OrderBy(p => p.City);
                case "rcity":
                case "icity":
                case "dcity":
                    return people.OrderByDescending(p => p.City);
            }
            return people;
        }

        static bool TestName(Person person, string filter) => person.Name != null && person.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        static bool TestCity(Person person, string filter) => person.City != null && person.City.Contains(filter, StringComparison.CurrentCultureIgnoreCase);

        public IEnumerator<KeyValuePair<int, Person>> GetEnumerator() => Filtered().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Filtered().GetEnumerator();

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

/*

    // Filter system frm old controller, kept here for refference.


 IEnumerable<Person> filtered = people;
            foreach (var query in filter.Split(' '))
            {
                if(query.Length > 2 && query[1] == ':')
                {
                    switch(query[0])
                    {
                        case 's':
                        case 'o':
                            if (query.Substring(2).ToLower() == "name")
                                filtered = filtered.OrderBy(p => p.Name);
                            else if (query.Substring(2).ToLower() == "age")
                                filtered = filtered.OrderBy(p => p.Age);
                            else if (query.Substring(2).ToLower() == "rage")
                                filtered = filtered.OrderBy(p => -p.Age);
                            else if (query.Substring(2).ToLower() == "iage")
                                filtered = filtered.OrderBy(p => -p.Age);
                            else if (query.Substring(2).ToLower() == "city")
                                filtered = filtered.OrderBy(p => p.City);
                            break;
                    }
                }
                else
                {
                    filtered = filtered.Where(person =>
                    {
                        return person.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase)
                        || (person.City != null && person.City.Contains(query, StringComparison.CurrentCultureIgnoreCase));
                    });
                }
            }


    */