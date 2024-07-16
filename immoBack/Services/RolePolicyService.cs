using immoBack.Models;

public interface IRolePolicyService
{
    Task<Dictionary<string, string>> GetRolePoliciesAsync();
}

public class RolePolicyService : IRolePolicyService
{
    private readonly IConfiguration _configuration;

    public RolePolicyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<Dictionary<string, string>> GetRolePoliciesAsync()
    {
        List<Role> roles=Role.GetRoles();
        var policies = new Dictionary<string, string>();
        foreach (var role in roles){
            var key=role.intitule+"Policy";
            policies.Add(key,role.intitule);
        }
        return Task.FromResult(policies);
    }
}
