using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using SchedulerWebApplication.Models;
using SchedulerWebApplication.Mutations;
using SchedulerWebApplication.Subscriptions;

namespace SchedulerWebApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(options =>
                    options.AddDefaultPolicy(policy =>
                        policy.WithOrigins("*").AllowAnyHeader()
                    )
                )
                .AddRouting()
                .AddEntityFrameworkSqlite()
                .AddDbContext<SchedulerContext>()
                .AddGraphQLServer()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions()
                .AddApolloTracing();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();
            app.UseWebSockets();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGraphQL();
                endpoints.MapGraphQLPlayground();
            });
            
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SchedulerContext>();
                //if (context.Database.EnsureCreated())
                //{
                    context.Executors.Clear();
                    context.Persons.Clear();
                    context.SaveChanges();

                    var person1 = new Person 
                    { 
                        Login = "testLogin",
                    };

                    context.LocalAccounts.Add(new LocalAccount 
                    { 
                        Person = person1, 
                        Password = "testPassword" 
                    });

                    context.MicrosoftAccounts.Add(new MicrosoftAccount
                    {
                        Person = person1,
                        MicrosoftAccountId = Guid.Parse("b2c4e305-63ed-431b-baef-bb051f70d749")
                    });
                    
                    var executor1 = context.Executors.Add(new Executor
                    {
                        Person = person1,
                        Name = "testName",
                        Description = "testDescription"
                    });
                    
                    context.Executors.Add(new Executor
                    {
                        Person = person1,
                        Name = "testName2",
                        Description = "testDescription2"
                    });
                    
                    var person2 = new Person
                    {
                        Login = "testLogin2",
                    };

                    context.LocalAccounts.Add(new LocalAccount
                    {
                        Person = person2,
                        Password = "testPassword2"
                    });

                    var task1 = context.Tasks.Add(new Task
                    {
                        InputType = typeof(string).ToString(),
                        OutputType = typeof(void).ToString(),
                        Name = "PrintText",
                        Command = "Write-Output $Env:text",
                        DefaultEnvironmentVariables = new Dictionary<string, string>(){{"text", "Hello world!"}}
                    });
                        
                    var task2 = context.Tasks.Add(new Task
                    {
                        InputType = typeof(string).ToString(),
                        OutputType = typeof(void).ToString(),
                        Name = "PrintSum",
                        Command = "[int]$Env:a + [int]$Env:b | Write-Output ",
                        DefaultEnvironmentVariables = new Dictionary<string, string>(){{"a", "1"}, {"b", "2"}}
                    });

                    context.SaveChanges();
                    var flowTask1 = context.FlowTasks.Add(new FlowTask
                    {
                        TaskId = task1.Entity.Id,
                        EnvironmentVariables = new Dictionary<string, string>(){{"text", "test1"}}
                    });
                    
                    var flowTask2 = context.FlowTasks.Add(new FlowTask
                    {
                        TaskId = task2.Entity.Id,
                        EnvironmentVariables = new Dictionary<string, string>(){{"a", "4"}, {"b", "15"}}
                    });

                    var flowTask3 = context.FlowTasks.Add(new FlowTask
                    {
                        TaskId = task1.Entity.Id,
                        EnvironmentVariables = new Dictionary<string, string>(){{"text", "Hello test!"}}
                    });
                    
                    context.ExecutorStatuses.Add(new ExecutorStatus{Date = 123, StatusCode = ExecutorStatusCode.Online, ExecutorId = executor1.Entity.Id});
                    context.ExecutorStatuses.Add(new ExecutorStatus{Date = 121, StatusCode = ExecutorStatusCode.Offline, ExecutorId = executor1.Entity.Id});
                    context.ExecutorStatuses.Add(new ExecutorStatus{Date = 124, StatusCode = ExecutorStatusCode.Working, ExecutorId = executor1.Entity.Id});

                    context.SaveChanges();
                    context.StartingUps.Add(new StartingUp
                    {
                        PredecessorId = flowTask1.Entity.Id,
                        SuccessorId = flowTask2.Entity.Id
                    });
                    
                    context.StartingUps.Add(new StartingUp
                    {
                        PredecessorId = flowTask2.Entity.Id,
                        SuccessorId = flowTask3.Entity.Id
                    });
                    
                    var flow = context.Flows.Add(new Flow
                    {
                        PersonId = person1.Id,
                        Description = "testDescription",
                        Name = "testName",
                        FlowTaskId = flowTask1.Entity.Id
                    });
                    
                    context.SaveChangesAsync();

                    int flowId = flow.Entity.Id;
                    int executorId = executor1.Entity.Id;
                    context.FlowRuns.Add(new FlowRun
                    {
                        RunDate = new DateTime(2012, 10, 20, 10, 15, 21).Ticks,
                        FlowId = flowId,
                        ExecutorId = executorId
                    });
                    context.FlowRuns.Add(new FlowRun
                    {
                        RunDate = new DateTime(2012, 10, 20, 11, 15, 21).Ticks,
                        FlowId = flowId,
                        ExecutorId = executorId
                    });
                    context.FlowRuns.Add(new FlowRun
                    {
                        RunDate = new DateTime(2012, 10, 20, 11, 30, 21).Ticks,
                        FlowId = flowId,
                        ExecutorId = executorId
                    });

                    context.SaveChangesAsync();
                //}
            }
        }
    }
}