﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchedulerWebApplication;

#nullable disable

namespace SchedulerWebApplication.Migrations
{
    [DbContext(typeof(SchedulerContext))]
    partial class SchedulerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("SchedulerWebApplication.Models.Executor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Executors");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.ExecutorStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExecutorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatusCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.ToTable("ExecutorStatuses");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Flow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("FlowTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FlowTaskId")
                        .IsUnique();

                    b.HasIndex("PersonId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowRun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExecutorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlowId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RunDate")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("FlowId");

                    b.ToTable("FlowRuns");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EnvironmentVariables")
                        .HasColumnType("TEXT");

                    b.Property<int>("TaskId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("FlowTasks");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowTaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("FlowRunId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlowTaskId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatusCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FlowRunId");

                    b.HasIndex("FlowTaskId");

                    b.ToTable("FlowTaskStatuses");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.LocalAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("LocalAccounts");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.MicrosoftAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("MicrosoftAccountId")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("MicrosoftAccounts");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.StartingUp", b =>
                {
                    b.Property<int>("PredecessorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SuccessorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PredecessorId", "SuccessorId");

                    b.HasIndex("SuccessorId");

                    b.ToTable("StartingUps");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Command")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("DefaultEnvironmentVariables")
                        .HasColumnType("TEXT");

                    b.Property<string>("InputType")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("OutputType")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Executor", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.Person", "Person")
                        .WithMany("Executors")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.ExecutorStatus", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.Executor", null)
                        .WithMany("Statuses")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Flow", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.FlowTask", "FlowTask")
                        .WithOne("Flow")
                        .HasForeignKey("SchedulerWebApplication.Models.Flow", "FlowTaskId");

                    b.HasOne("SchedulerWebApplication.Models.Person", "Person")
                        .WithMany("Flows")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlowTask");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowRun", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.Executor", "Executor")
                        .WithMany("FlowRuns")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchedulerWebApplication.Models.Flow", "Flow")
                        .WithMany("FlowRuns")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Executor");

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowTask", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.Task", "Task")
                        .WithMany("FlowTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowTaskStatus", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.FlowRun", null)
                        .WithMany("Statuses")
                        .HasForeignKey("FlowRunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchedulerWebApplication.Models.FlowTask", null)
                        .WithMany()
                        .HasForeignKey("FlowTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.LocalAccount", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.MicrosoftAccount", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.StartingUp", b =>
                {
                    b.HasOne("SchedulerWebApplication.Models.FlowTask", "Predecessor")
                        .WithMany("Successors")
                        .HasForeignKey("PredecessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SchedulerWebApplication.Models.FlowTask", "Successor")
                        .WithMany("Predecessors")
                        .HasForeignKey("SuccessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Predecessor");

                    b.Navigation("Successor");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Executor", b =>
                {
                    b.Navigation("FlowRuns");

                    b.Navigation("Statuses");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Flow", b =>
                {
                    b.Navigation("FlowRuns");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowRun", b =>
                {
                    b.Navigation("Statuses");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.FlowTask", b =>
                {
                    b.Navigation("Flow");

                    b.Navigation("Predecessors");

                    b.Navigation("Successors");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Person", b =>
                {
                    b.Navigation("Executors");

                    b.Navigation("Flows");
                });

            modelBuilder.Entity("SchedulerWebApplication.Models.Task", b =>
                {
                    b.Navigation("FlowTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
