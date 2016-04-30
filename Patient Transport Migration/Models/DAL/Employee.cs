namespace Patient_Transport_Migration.Models.DAL {
    public partial class Employee {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get { return FirstName + " " + LastName; } }
    }
}