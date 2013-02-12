using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serialization
{
    public class TestClass
    {
        public string _name = "Jonathan";
        public string _school = "Aurora University";

        public TestClass()
        {
        }

        public TestClass(string name, string school)
        {
            _name = name;
            _school = school;
        }
    }
}