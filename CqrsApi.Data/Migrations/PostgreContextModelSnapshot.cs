﻿// <auto-generated />
using System;
using CqrsApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CqrsApi.Data.Migrations
{
    [DbContext(typeof(PostgreContext))]
    partial class PostgreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("CqrsApi.Models.Models.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("actor_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birthday");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.HasKey("ActorId");

                    b.ToTable("actors");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("client_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birthday");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.HasKey("ClientId");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Copy", b =>
                {
                    b.Property<int>("CopyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("copy_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("Available")
                        .HasColumnType("boolean")
                        .HasColumnName("available");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.HasKey("CopyId");

                    b.HasIndex(new[] { "MovieId" }, "IX_copies_movie_id");

                    b.ToTable("copies");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("employee_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<float?>("Salary")
                        .HasColumnType("real")
                        .HasColumnName("salary");

                    b.HasKey("EmployeeId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("movie_id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AgeRestriction")
                        .HasColumnType("integer")
                        .HasColumnName("age_restriction");

                    b.Property<float>("Price")
                        .HasColumnType("real")
                        .HasColumnName("price");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("MovieId");

                    b.ToTable("movies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            AgeRestriction = 12,
                            Price = 10f,
                            Title = "Star Wars Episode IV: A New Hope",
                            Year = 1979
                        },
                        new
                        {
                            MovieId = 2,
                            AgeRestriction = 12,
                            Price = 5.5f,
                            Title = "Ghostbusters",
                            Year = 1984
                        },
                        new
                        {
                            MovieId = 3,
                            AgeRestriction = 15,
                            Price = 8.5f,
                            Title = "Terminator",
                            Year = 1984
                        },
                        new
                        {
                            MovieId = 4,
                            AgeRestriction = 17,
                            Price = 5f,
                            Title = "Taxi Driver",
                            Year = 1976
                        },
                        new
                        {
                            MovieId = 5,
                            AgeRestriction = 18,
                            Price = 5f,
                            Title = "Platoon",
                            Year = 1986
                        },
                        new
                        {
                            MovieId = 6,
                            AgeRestriction = 15,
                            Price = 8.5f,
                            Title = "Frantic",
                            Year = 1988
                        },
                        new
                        {
                            MovieId = 7,
                            AgeRestriction = 13,
                            Price = 9.5f,
                            Title = "Ronin",
                            Year = 1998
                        },
                        new
                        {
                            MovieId = 8,
                            AgeRestriction = 16,
                            Price = 10.5f,
                            Title = "Analyze This",
                            Year = 1999
                        },
                        new
                        {
                            MovieId = 9,
                            AgeRestriction = 16,
                            Price = 8.5f,
                            Title = "Leon: the Professional",
                            Year = 1994
                        },
                        new
                        {
                            MovieId = 10,
                            AgeRestriction = 13,
                            Price = 8.5f,
                            Title = "Mission Impossible",
                            Year = 1996
                        });
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Rental", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<int>("CopyId")
                        .HasColumnType("integer")
                        .HasColumnName("copy_id");

                    b.Property<DateTime?>("DateOfRental")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_rental");

                    b.Property<DateTime?>("DateOfReturn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_return");

                    b.HasKey("ClientId", "CopyId")
                        .HasName("rentals_pkey");

                    b.HasIndex(new[] { "CopyId" }, "IX_rentals_copy_id");

                    b.ToTable("rentals");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Starring", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("integer")
                        .HasColumnName("actor_id");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex(new[] { "MovieId" }, "IX_starring_movie_id");

                    b.ToTable("starring");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Copy", b =>
                {
                    b.HasOne("CqrsApi.Models.Models.Movie", "Movie")
                        .WithMany("Copies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Rental", b =>
                {
                    b.HasOne("CqrsApi.Models.Models.Client", "Client")
                        .WithMany("Rentals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CqrsApi.Models.Models.Copy", "Copy")
                        .WithMany("Rentals")
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Copy");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Starring", b =>
                {
                    b.HasOne("CqrsApi.Models.Models.Actor", "Actor")
                        .WithMany("Starrings")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CqrsApi.Models.Models.Movie", "Movie")
                        .WithMany("Starrings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Actor", b =>
                {
                    b.Navigation("Starrings");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Client", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Copy", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("CqrsApi.Models.Models.Movie", b =>
                {
                    b.Navigation("Copies");

                    b.Navigation("Starrings");
                });
#pragma warning restore 612, 618
        }
    }
}