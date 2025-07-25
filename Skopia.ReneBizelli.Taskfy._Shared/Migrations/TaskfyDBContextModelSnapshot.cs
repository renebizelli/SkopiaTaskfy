﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

#nullable disable

namespace Skopia.ReneBizelli.Taskfy._Shared.Migrations
{
    [DbContext(typeof(TaskfyDBContext))]
    partial class TaskfyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectsUsers", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ProjectsUsers");
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("TaskItemsLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<DateTime?>("DoneAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<byte>("Priority")
                        .HasColumnType("TinyInt");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<byte>("Status")
                        .HasColumnType("TinyInt");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskItems", (string)null);
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.TaskItemHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<byte>("Type")
                        .HasColumnType("TinyInt");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskItemHistoty", (string)null);
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<byte>("Role")
                        .HasColumnType("TinyInt");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ProjectsUsers", b =>
                {
                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.Project", b =>
                {
                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.User", "Author")
                        .WithMany("AuthoredProjects")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.TaskItem", b =>
                {
                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.TaskItemHistory", b =>
                {
                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.TaskItem", "Task")
                        .WithMany("History")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skopia.ReneBizelli.Taskfy._Shared.Entities.User", "User")
                        .WithMany("History")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.TaskItem", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("Skopia.ReneBizelli.Taskfy._Shared.Entities.User", b =>
                {
                    b.Navigation("AuthoredProjects");

                    b.Navigation("History");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
