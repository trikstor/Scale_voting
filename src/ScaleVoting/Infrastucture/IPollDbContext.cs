using System.Data.Entity;
using ScaleVoting.Models;

namespace ScaleVoting.Infrastucture
{
    public interface IPollDbContext
    {
        DbSet<Poll> Polls { get; set; }
        DbSet<Option> Options { get; set; }
    }
}
