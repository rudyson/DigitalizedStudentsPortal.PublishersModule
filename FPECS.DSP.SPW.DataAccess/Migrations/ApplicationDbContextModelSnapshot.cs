﻿// <auto-generated />
using System;
using FPECS.DSP.SPW.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FPECS.DSP.SPW.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Chair", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("abbreviation");

                    b.Property<long>("FacultyId")
                        .HasColumnType("bigint")
                        .HasColumnName("faculty_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_chairs");

                    b.HasIndex("FacultyId")
                        .HasDatabaseName("ix_chairs_faculty_id");

                    b.ToTable("chairs", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Faculty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_faculties");

                    b.ToTable("faculties", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Publication", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Category")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1)
                        .HasColumnName("category");

                    b.Property<string>("Doi")
                        .HasColumnType("text")
                        .HasColumnName("doi");

                    b.Property<string>("Isbn")
                        .HasColumnType("text")
                        .HasColumnName("isbn");

                    b.Property<string>("Issn")
                        .HasColumnType("text")
                        .HasColumnName("issn");

                    b.Property<short>("Pages")
                        .HasColumnType("smallint")
                        .HasColumnName("pages");

                    b.Property<short?>("PagesAuthor")
                        .HasColumnType("smallint")
                        .HasColumnName("pages_author");

                    b.Property<string>("PublicationOriginSource")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("publication_origin_source");

                    b.Property<string>("PublicationOriginSourceUrl")
                        .HasColumnType("text")
                        .HasColumnName("publication_origin_source_url");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_publications");

                    b.ToTable("publications", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.PublicationPublisher", b =>
                {
                    b.Property<long>("PublicationId")
                        .HasColumnType("bigint")
                        .HasColumnName("publication_id");

                    b.Property<long>("PublisherId")
                        .HasColumnType("bigint")
                        .HasColumnName("publisher_id");

                    b.Property<long>("PseudonymId")
                        .HasColumnType("bigint")
                        .HasColumnName("pseudonym_id");

                    b.Property<short?>("PublicationNumber")
                        .HasColumnType("smallint")
                        .HasColumnName("publication_number");

                    b.HasKey("PublicationId", "PublisherId", "PseudonymId")
                        .HasName("pk_publications_publishers");

                    b.HasIndex("PseudonymId")
                        .HasDatabaseName("ix_publications_publishers_pseudonym_id");

                    b.HasIndex("PublisherId")
                        .HasDatabaseName("ix_publications_publishers_publisher_id");

                    b.ToTable("publications_publishers", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Researcher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<long?>("NppId")
                        .HasColumnType("bigint")
                        .HasColumnName("npp_id");

                    b.Property<long?>("OrcId")
                        .HasColumnType("bigint")
                        .HasColumnName("orc_id");

                    b.HasKey("Id")
                        .HasName("pk_researchers");

                    b.ToTable("researchers", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.ResearcherProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("InternalId")
                        .HasColumnType("text")
                        .HasColumnName("internal_id");

                    b.Property<long>("ResearcherId")
                        .HasColumnType("bigint")
                        .HasColumnName("researcher_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_researcher_profiles");

                    b.HasIndex("ResearcherId")
                        .HasDatabaseName("ix_researcher_profiles_researcher_id");

                    b.ToTable("researcher_profiles", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.ResearcherPseudonym", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<long>("ResearcherId")
                        .HasColumnType("bigint")
                        .HasColumnName("researcher_id");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("short_name");

                    b.HasKey("Id")
                        .HasName("pk_researcher_pseudonyms");

                    b.HasIndex("ResearcherId")
                        .HasDatabaseName("ix_researcher_pseudonyms_researcher_id");

                    b.ToTable("researcher_pseudonyms", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.ScienceEmployee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AcademicDegree")
                        .HasColumnType("integer")
                        .HasColumnName("academic_degree");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<long>("ChairId")
                        .HasColumnType("bigint")
                        .HasColumnName("chair_id");

                    b.Property<string>("OrcidUrl")
                        .HasColumnType("text")
                        .HasColumnName("orcid_url");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Posada")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("posada");

                    b.Property<string>("Stepin")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("stepin");

                    b.Property<string>("Zvannya")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("zvannya");

                    b.HasKey("Id")
                        .HasName("pk_science_employee");

                    b.HasIndex("ChairId")
                        .HasDatabaseName("ix_science_employee_chair_id");

                    b.ToTable("science_employee", (string)null);
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Chair", b =>
                {
                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.Faculty", "Faculty")
                        .WithMany("Chairs")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_chairs_faculties_faculty_id");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.PublicationPublisher", b =>
                {
                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.ResearcherPseudonym", "Pseudonym")
                        .WithMany()
                        .HasForeignKey("PseudonymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_publications_publishers_researcher_pseudonyms_pseudonym_id");

                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.Publication", "Publication")
                        .WithMany("PublicationPublishers")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_publications_publishers_publications_publication_id");

                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.Researcher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_publications_publishers_researchers_publisher_id");

                    b.Navigation("Pseudonym");

                    b.Navigation("Publication");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.ResearcherProfile", b =>
                {
                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.Researcher", "Researcher")
                        .WithMany("ResearcherProfiles")
                        .HasForeignKey("ResearcherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_researcher_profiles_researchers_researcher_id");

                    b.Navigation("Researcher");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.ResearcherPseudonym", b =>
                {
                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.Researcher", "Researcher")
                        .WithMany("ResearcherPseudonyms")
                        .HasForeignKey("ResearcherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_researcher_pseudonyms_researchers_researcher_id");

                    b.Navigation("Researcher");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.ScienceEmployee", b =>
                {
                    b.HasOne("FPECS.DSP.SPW.DataAccess.Entities.Chair", "Chair")
                        .WithMany("ScienceEmployees")
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_science_employee_chairs_chair_id");

                    b.Navigation("Chair");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Chair", b =>
                {
                    b.Navigation("ScienceEmployees");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Faculty", b =>
                {
                    b.Navigation("Chairs");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Publication", b =>
                {
                    b.Navigation("PublicationPublishers");
                });

            modelBuilder.Entity("FPECS.DSP.SPW.DataAccess.Entities.Researcher", b =>
                {
                    b.Navigation("ResearcherProfiles");

                    b.Navigation("ResearcherPseudonyms");
                });
#pragma warning restore 612, 618
        }
    }
}
