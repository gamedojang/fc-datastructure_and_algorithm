using LinkedList;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace LinkedList
{
    public class Person
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
    }
}
