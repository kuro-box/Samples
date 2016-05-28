using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models
{
    public class Person
    {
        public string PersonId { get; set; }

        [Required]
        public string Name { get; set; }

        public static IEnumerable<Person> Persons
        {
            get
            {
                return new List<Person>
                {
                    new Person { PersonId = "000", Name = "Admin" },
                    new Person { PersonId = "001", Name = "Tom" },
                    new Person { PersonId = "002", Name = "John" },
                    new Person { PersonId = "003", Name = "King" },
                    new Person { PersonId = "004", Name = "Snake" }
                };
            }
        }
    }
}