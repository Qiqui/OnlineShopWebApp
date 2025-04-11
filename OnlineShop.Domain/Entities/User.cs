using OnlineShop.Domain.Common;

namespace OnlineShop.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? ImagePath { get; set; }
        public List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

        private User(Guid id, string? name, string? surname, int? age, string? imagePath)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            ImagePath = imagePath;
        }

        public static User Create(
        Guid id,
        string? name = null,
        string? surname = null,
        int? age = null,
        string? imagePath = null)
        {
            if(age < 0)
            { }
            return new User(id, name, surname, age, imagePath);
        }
    }
}

