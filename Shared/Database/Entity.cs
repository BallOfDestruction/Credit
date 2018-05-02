using Newtonsoft.Json;

namespace Shared.Database
{
    public class Entity : IEntity
    {
        [JsonProperty("LocalId")]
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
    }
}
