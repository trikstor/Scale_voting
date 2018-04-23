using System;
using System.Collections.Generic;
using System.Data.Entity;
using ScaleVoting.Models;

namespace ScaleVoting.Infrastucture
{
    public class PollDbContext : DbContext
    {
        public PollDbContext() : base("name=PollDatabase")
        { }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}