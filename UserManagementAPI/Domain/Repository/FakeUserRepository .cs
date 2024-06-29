namespace UserManagementAPI.Domain.Repository
{
    public class FakeUserRepository: IUserRepository
    {
        private readonly List<User> _users;

        public FakeUserRepository()
        {
            _users = new List<User>
             {
              new User { Id = 1, Email = "lailaalkhadrawy83@gmail.com", PhoneNumber = "+46735285537", Password = "Password1!" },
              new User { Id = 2, Email = "user2@example.com", PhoneNumber = "+46735288877", Password = "Password2!" }
             };
        }

        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public User GetByPhoneNumber(string phoneNumber)
        {
            return _users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public void Add(User user)
        {
            user.Id = _users.Count + 1; // Generate new ID (in a real scenario, this would be handled by the database)
            _users.Add(user);
        }
    }
}
