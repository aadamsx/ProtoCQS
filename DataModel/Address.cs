namespace DataModel
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class County
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateProvinceId { get; set; }

        //[ForeignKey("StateProvinceId")]
        public virtual StateProvince StateProvince { get; set; }
    }

    public class StateProvince
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
