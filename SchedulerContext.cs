using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SchedulerWebApplication.Models;

namespace SchedulerWebApplication
{
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }

    public class SchedulerContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public SchedulerContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<LocalAccount> LocalAccounts { get; set; }
        public DbSet<MicrosoftAccount> MicrosoftAccounts { get; set; }

        public DbSet<Executor> Executors { get; set; }
        
        public DbSet<ExecutorStatus> ExecutorStatuses { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowRun> FlowRuns { get; set; }
        public DbSet<FlowTask> FlowTasks { get; set; }
        public DbSet<StartingUp> StartingUps { get; set; }
        public DbSet<FlowTaskStatus> FlowTaskStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=LocalDatabase.db");
            //options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING")); //(@$"User Id=system;Password=oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={Environment.GetEnvironmentVariable("DB_HOST") ?? "scheduler-oracle-db.westeurope.azurecontainer.io"})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)))");
            options.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Task>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            var dictionariesComparer = new ValueComparer<Dictionary<string, string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToDictionary());

            modelBuilder.Entity<FlowTask>()
                .Property(b => b.EnvironmentVariables)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v),
                    dictionariesComparer);
            
            modelBuilder.Entity<Task>()
                .Property(b => b.DefaultEnvironmentVariables)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v),
                    dictionariesComparer);

            modelBuilder.Entity<Person>()
                .HasMany(t => t.Executors)
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId);

            modelBuilder.Entity<Person>()
                .HasMany<LocalAccount>()
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasMany<MicrosoftAccount>()
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Executor>()
                .HasMany(t => t.Statuses)
                .WithOne()
                .HasForeignKey(t => t.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Person>()
                .HasMany(t => t.Flows)
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                .HasMany(t => t.FlowTasks)
                .WithOne(t => t.Task)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlowTask>()
                .HasOne(t => t.Flow)
                .WithOne(t => t.FlowTask)
                .HasForeignKey<Flow>(t => t.FlowTaskId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<StartingUp>()
                .HasKey(p => new {p.PredecessorId, p.SuccessorId});

            modelBuilder.Entity<StartingUp>()
                .HasOne(t => t.Predecessor)
                .WithMany(t => t.Successors)
                .HasForeignKey(t => t.PredecessorId)    
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StartingUp>()
                .HasOne(t => t.Successor)
                .WithMany(t => t.Predecessors)
                .HasForeignKey(t => t.SuccessorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlowRun>()
                .HasOne(f => f.Executor)
                .WithMany(e => e.FlowRuns)
                .HasForeignKey(f => f.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlowRun>()
                .HasOne(f => f.Flow)
                .WithMany(e => e.FlowRuns)
                .HasForeignKey(f => f.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlowTaskStatus>()
                .HasOne<FlowRun>()
                .WithMany(f => f.Statuses)
                .HasForeignKey(e => e.FlowRunId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlowTaskStatus>()
                .HasOne<FlowTask>()
                .WithMany()
                .HasForeignKey(f => f.FlowTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            /*modelBuilder.Entity<FlowTask>()
                .HasMany<FlowTask>(t => t.Predecessors)
                .WithMany(t => t.Successors)
                .UsingEntity<StartingUp>(
                    typeof(StartingUp), 
                    t=> t.HasOne<FlowTask>(t => t.Predecessor).WithMany(t => t.Predecessors).HasForeignKey(t => t.PredecessorId),
                    t=> t.HasOne<FlowTask>(t => t.Predecessor).WithMany(t => t.Predecessors).HasForeignKey(t => t.PredecessorId),
                    );
            */
        }
    }
}
