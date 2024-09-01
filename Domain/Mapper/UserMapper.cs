using Domain.DTOs;
using Domain.DTOs.Account;
using Domain.DTOs.UserDtos;
using Domain.Entities;

namespace Domain.Mapper;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        var userDto = new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        };
        return userDto;
    }
    
    public static User ToUser(this UserDto user)
    {
        var userMapped = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        };
        
        return userMapped;
    }
}