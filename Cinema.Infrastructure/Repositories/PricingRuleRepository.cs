using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;

namespace Cinema.Infrastructure.Repositories
{
    public class PricingRuleRepository : Repository<PricingRule, int>, IPricingRuleRepository
    {
        public PricingRuleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}