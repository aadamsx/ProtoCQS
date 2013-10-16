using System;

namespace Proto.Model.Entities
{
    public class County
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int StateProvinceId { get; set; }

        //[ForeignKey("StateProvinceId")]
        public virtual StateProvince StateProvince { get; set; }
    }
}
