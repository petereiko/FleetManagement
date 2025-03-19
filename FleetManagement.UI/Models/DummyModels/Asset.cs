namespace FleetManagement.UI.Models.DummyModels
{
    // Models/Asset.cs
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
    }

    // Models/Staff.cs
    public class Staff
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }

    // Models/Report.cs
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateFiled { get; set; }
    }

}
