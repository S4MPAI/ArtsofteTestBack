using DAL.Base;
using DAL.Entities;

namespace DAL.Contexts
{
    public class InApplicationMemoryContext
    {
        public InMemorySet<User> Users { get; set; } = new InMemorySet<User>(new List<User>());
    }
}
