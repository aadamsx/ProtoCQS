using System;

namespace ProtoConsole
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid RowGuild { get; set; }
        public byte[] RowVersion { get; set; }
    }
}