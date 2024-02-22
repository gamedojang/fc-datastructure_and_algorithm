using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace LinkedList
{
    public class Person : IComparable<Person>, IEnumerable<Person>
    {
        public enum GenderType
        {
            Male, Female        
        }
    
        public string Name;
        public int Age;
        public GenderType Gender;
        public string Job;
        
        private IEnumerator<Person> enumerator;

        public Person(string name, int age, GenderType gender, string job)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Job = job;
        }
        public int CompareTo(Person other)
        {
            return -1;
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
