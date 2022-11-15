using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("This username is already taken");
        using var hmac = new HMACSHA256();
        var user = new AppUser
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
        return user;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync( user => user.UserName.ToLower().Equals(loginDto.Username.ToLower()));

        if (user == null) return Unauthorized("Invalid username");

        using (var hmac = new HMACSHA256(user.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++) 
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return user;
    }
    
    
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(user => user.UserName.ToLower().Equals(username.ToLower()));
    }
}