using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class BaseDbContext : IdentityDbContext<UserEntity>
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        #region Tables

        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<QuestionEntity> QuestionEntities { get; set; }
        public DbSet<AnswerEntity> AnswerEntities { get; set; }

        #endregion
    }
}
