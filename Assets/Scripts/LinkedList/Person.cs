using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace LinkedList
{
    public class Person : IComparable<Person>
    {
        public enum GenderType
        {
            Male, Female        
        }
    
        public string Name;
        public int Age;
        public GenderType Gender;
        public string Job;

        public Person(string name, int age, GenderType gender, string job)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Job = job;
        }
        public int CompareTo(Person other)
        {
            if ((Name == other.Name) &&
                (Age == other.Age) &&
                (Gender == other.Gender) &&
                (Job == other.Job))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
