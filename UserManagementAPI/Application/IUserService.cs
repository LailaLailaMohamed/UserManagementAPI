using System.Collections.Generic;
using UserManagementAPI.Application;
using UserManagementAPI.Domain;

public interface IUserService
{
    User CreateUser(CreateUserRequest createUserRequest);
    User GetUserByEmail(string email);
    User GetUserByPhoneNumber(string phoneNumber);
}