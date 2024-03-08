using System;

namespace PriorityQueue
{
    public class Person2 : IComparable<Person2>
    {
        public enum GenderType
        {
            Male, Female
        }

        public string Name;
        public int Age;
        public GenderType Gender;
        public string Job;

        public Person2(string name, int age, GenderType gender, string job)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Job = job;
        }

        public int CompareTo(Person2 other)
        {
            Person2 person = obj as Person2;
            if (person != null)
            {
                return Name.CompareTo(person.Name);
            }
            else
            {
                throw new ArgumentException("Person °´Ã¼°¡ ¾Æ´Õ´Ï´Ù.");
            }
        }
    }
}
