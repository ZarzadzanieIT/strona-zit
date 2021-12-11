using System.Net;
using Microsoft.EntityFrameworkCore;
using ZIT.Core.DTOs;
using ZIT.Core.Entities;
using ZIT.Core.Services;
using ZIT.Infrastructure.Authorization;
using ZIT.Infrastructure.Persistence;

namespace ZIT.Infrastructure.Services;

public class UserService : IAuthService, IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private async Task<(List<ApplicationRole> roles, List<string> entitlements)> GetAuthorizationProps(
        List<string>? roleNames, List<string>? entitlements)
    {
        var roleNamesInvariant = roleNames?
            .Select(x => x.ToLowerInvariant()).ToHashSet() ?? new HashSet<string>();
        var roles = await _dbContext.Roles
            .Include(x => x.ChildrenRoles)
            .Where(x => roleNamesInvariant
                .Contains(x.Name!.ToLowerInvariant()))
            .ToListAsync();
        var userSpecificEntitlements =
            entitlements?
                .Except(roles
                    .SelectMany(x => x.AllEntitlements))
                .ToList() ?? new List<string>();

        return (roles, userSpecificEntitlements);
    }

    public async Task AddAsync(AddUserDto addUserDto)
    {
        var (roles, userSpecificEntitlements) = await GetAuthorizationProps(addUserDto.Roles, addUserDto.Entitlements);

        var user = new ApplicationUser(addUserDto.Name, addUserDto.Email,
            PasswordHelper.CalculateHash(addUserDto.Password), roles, userSpecificEntitlements);
        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserDto?> GetAsync(Guid id)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.ChildrenRoles
            )
            .SingleOrDefaultAsync(x => x.Id == id);

        return user switch
        {
            { } => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles?.Select(x => x.Name).ToList(),
                Entitlements = user.Entitlements!.ToList(),
                AllEntitlements = user.AllEntitlements.ToList()
            },
            _ => null
        };
    }

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.ChildrenRoles
            )
            .SingleOrDefaultAsync(x => x.Email!.Equals(email, StringComparison.InvariantCultureIgnoreCase));

        return user switch
        {
            { } => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles?.Select(x => x.Name).ToList(),
                Entitlements = user.Entitlements!.ToList(),
                AllEntitlements = user.AllEntitlements.ToList()
            },
            _ => null
        };
    }

    public async Task<UserDto?> GetByNameAsync(string name)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.ChildrenRoles
            )
            .SingleOrDefaultAsync(x => x.Name!.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        return user switch
        {
            { } => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles?.Select(x => x.Name).ToList(),
                Entitlements = user.Entitlements!.ToList(),
                AllEntitlements = user.AllEntitlements.ToList()
            },
            _ => null
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _dbContext.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.ChildrenRoles)
            .ToListAsync();

        return users.Select(x => new UserDto
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            Roles = x.Roles?.Select(y => y.Name).ToList(),
            Entitlements = x.Entitlements!.ToList(),
            AllEntitlements = x.AllEntitlements.ToList()
        });
    }

    public async Task<ApplicationResult<UserDto?>> LoginAsync(LoginDto loginDto)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.ChildrenRoles
            )
            .SingleOrDefaultAsync(x => x.Email!.Equals(loginDto.Email, StringComparison.InvariantCultureIgnoreCase));

        return user switch
        {
            { } when PasswordHelper.CheckMatch(user.Password, loginDto.Password) => ApplicationResult.Success(
                new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Roles = user.Roles?.Select(x => x.Name).ToList(),
                    Entitlements = user.Entitlements!.ToList(),
                    AllEntitlements = user.AllEntitlements.ToList()
                }, HttpStatusCode.OK),
            _ => ApplicationResult.Fail<UserDto>(HttpStatusCode.BadRequest, "Invalid credentials")
        };
    }

    public async Task UpdateAsync(UpdateUserDto updateUserDto)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles)!
            .ThenInclude(x => x.ChildrenRoles)
            .SingleOrDefaultAsync(x => x.Id == updateUserDto.Id);
        var (roles, userSpecificEntitlements) =
            await GetAuthorizationProps(updateUserDto.Roles, updateUserDto.Entitlements);

        user!.Entitlements = userSpecificEntitlements;
        user.Roles = roles;
        user.Name = updateUserDto.Name;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
        {
            user.Password = PasswordHelper.CalculateHash(updateUserDto.Password);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        _dbContext.Entry(user!).State = EntityState.Deleted;
    }
}