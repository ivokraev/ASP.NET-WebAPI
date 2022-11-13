using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;
    
    public UsersController(DataContext conext)
    {
        _context = conext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        return  _context.Users.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser?> GetUser(int id)
    {
        return  _context.Users.Find(id);
    }
}