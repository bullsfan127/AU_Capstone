namespace CustomSerialization
{
    public class TestClass
    {
        public string _name = "Jonathan";
        public string _school = "Aurora University";
        public string test2 = "public";
        private string test = "private";

        public TestClass()
        {
        }

        public TestClass(string name, string school)
        {
            _name = name;
            _school = school;
        }

        public string Test
        {
            get { return test; }
            set { test = value; }
        }
    }
}