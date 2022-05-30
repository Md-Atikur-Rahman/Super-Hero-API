using MongoDB.Bson;

namespace SuperHero
{
    public class SuperHero
    {
        public ObjectId id { get; set; }
        public int HeroId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
    }
}
