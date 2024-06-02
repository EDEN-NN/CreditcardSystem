using CreditcardSystem.Application.Dtos.Request;
using CreditcardSystem.Application.Dtos.Response;
using CreditcardSystem.Application.Exceptions;
using CreditcardSystem.Application.Repositories;
using CreditcardSystem.Domain.Models;

namespace CreditcardSystem.Application.Dtos.Services;

public class UserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        var usersResponse = new List<UserResponse>();
        var users = await _userRepository.GetAllUsers();
        users.ForEach(user =>
        {
            usersResponse.Add((UserResponse)user);
        });

        return usersResponse;
    }

    public async Task<UserResponse> GetUserById(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new UserNotFoundException(
                "Unable to find user with ID: " + userId,
                ExceptionType.NotFoundException
            );
        }
        return (UserResponse)user;
    }

    public async Task<UserResponse> SaveUser(UserRequest userRequest)
    {
        var user = await _userRepository.SaveUser((User)userRequest);
        return (UserResponse)user;
    }

    public async Task UpdateUser(UserRequest userRequest, Guid userId)
    {
        var user = (User)userRequest;
        await _userRepository.UpdateUser(user, userId);
    }

    public async Task DeleteUser(Guid userId)
    {
        await _userRepository.DeleteUser(userId);
    }
}
