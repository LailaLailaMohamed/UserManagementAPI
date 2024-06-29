using System.Net.Mail;
using UserManagementAPI.Domain;
using UserManagementAPI.Domain.Repository;

namespace UserManagementAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(CreateUserRequest createUserRequest)
        {
            // Normalize phone number
            var phoneNumber = createUserRequest.PhoneNumber;
            if (!phoneNumber.StartsWith("+"))
            {
                if (phoneNumber.StartsWith("00"))
                {
                    phoneNumber = "+" + phoneNumber.Substring(2);
                }
                else
                {
                    phoneNumber = "+46" + phoneNumber;
                }
            }

            var newUser = new User
            {
                Email = createUserRequest.Email,
                PhoneNumber = phoneNumber,
                Password = createUserRequest.Password
            };

            _userRepository.Add(newUser);

            SendNotification(newUser);

            return newUser;
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _userRepository.GetByPhoneNumber(phoneNumber);
        }

        private void SendNotification(User user)
        {
            if (!string.IsNullOrEmpty(user.Email))
            {
                SendEmail(user.Email, user);
            }
            else if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                SendTextMessage(user.PhoneNumber, user);
            }
        }

        private void SendEmail(string email, User user)
        {
            //In real life values should be substituted by correct values or we use third party service to send emails like (SendGrid)
            var fromAddress = new MailAddress("lailaalkhadrawy83@gmail.com", "gmail");
            var toAddress = new MailAddress(email);
            const string fromPassword = "yourpassword";
            const string subject = "Account Creation";
            string body = $"Your account has been created. \nEmail: {user.Email}\nPhone: {user.PhoneNumber}\nPassword: {user.Password}";

            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.example.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        private void SendTextMessage(string phoneNumber, User user)
        {
            // Placeholder for text message sending logic
            // I need to integrate with a third-party SMS service (e.g., Twilio)
            // This is a simplified example and won't actually send an SMS.

            string body = $"Your account has been created. \nPhone: {user.PhoneNumber}\nPassword: {user.Password}";
            System.Console.WriteLine($"Sending SMS to {phoneNumber}: {body}");
        }
    }
}
