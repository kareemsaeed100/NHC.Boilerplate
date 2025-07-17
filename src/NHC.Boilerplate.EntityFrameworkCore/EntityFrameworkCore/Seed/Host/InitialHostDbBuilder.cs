using Abp.Dependency;

namespace NHC.Boilerplate.EntityFrameworkCore.Seed.Host;

public class InitialHostDbBuilder
{
    private readonly BoilerplateDbContext _context;
    private readonly IIocResolver _IocResolver;

    public InitialHostDbBuilder(BoilerplateDbContext context, IIocResolver IocResolver)
    {
        _context = context;
        _IocResolver = IocResolver;
    }

    public void Create()
    {
        new DefaultEditionCreator(_context).Create();
        new DefaultLanguagesCreator(_context).Create();
        new HostRoleAndUserCreator(_context).Create();
        new DefaultSettingsCreator(_context, _IocResolver).Create();

        _context.SaveChanges();
    }
}
