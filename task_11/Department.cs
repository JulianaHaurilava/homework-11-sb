namespace task_11
{
    public class Department
    {
        public string Name { get; set; }

        public Department()
        {
            Name = "";
        }
        public Department(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
