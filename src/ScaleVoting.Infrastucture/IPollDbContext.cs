using System.Data.Entity;
using ScaleVoting.Models;

namespace ScaleVoting.Infrastucture
{
    public interface IPollDbContext
    {
        DbSet<Question> Questions { get; set; }
        DbSet<Option> Options { get; set; }
    }
}
