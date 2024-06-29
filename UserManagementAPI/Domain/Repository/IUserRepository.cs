namespace UserManagementAPI.Domain.Repository
{
        public interface IUserRepository
        {
            User GetByEmail(string email);
            User GetByPhoneNumber(string phoneNumber);
            void Add(User user);
        }
}
