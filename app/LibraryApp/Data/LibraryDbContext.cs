using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using LibraryApp.Models;

namespace LibraryApp.Data;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookDetail> BookDetails { get; set; }

    public virtual DbSet<Borrowed> Borroweds { get; set; }

    public virtual DbSet<BorrowedDetail> BorrowedDetails { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user id=root;password=pass;database=libraryDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("authors");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => e.AuthorId, "author_id");

            entity.HasIndex(e => e.BranchId, "branch_id");

            entity.HasIndex(e => e.GenreId, "genre_id");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Availability)
                .HasColumnType("enum('available','not available')")
                .HasColumnName("availability");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.PublishYear).HasColumnName("publish_year");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("books_ibfk_1");

            entity.HasOne(d => d.Branch).WithMany(p => p.Books)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("books_ibfk_3");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("books_ibfk_2");
        });

        modelBuilder.Entity<BookDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("book_details");

            entity.Property(e => e.Author)
                .HasMaxLength(101)
                .HasColumnName("author");
            entity.Property(e => e.Availability)
                .HasColumnType("enum('available','not available')")
                .HasColumnName("availability");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Branch)
                .HasMaxLength(50)
                .HasColumnName("branch");
            entity.Property(e => e.BranchAddress)
                .HasMaxLength(100)
                .HasColumnName("branch_address");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .HasColumnName("genre");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.PublishYear).HasColumnName("publish_year");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Borrowed>(entity =>
        {
            entity.HasKey(e => e.BorrowedId).HasName("PRIMARY");

            entity.ToTable("borrowed");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.BorrowedId).HasColumnName("borrowed_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");
            entity.Property(e => e.Charge).HasColumnName("charge");
            entity.Property(e => e.PlannedReturnDate).HasColumnName("planned_return_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Borroweds)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("borrowed_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Borroweds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("borrowed_ibfk_1");
        });

        modelBuilder.Entity<BorrowedDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("borrowed_details");

            entity.Property(e => e.Book)
                .HasMaxLength(50)
                .HasColumnName("book");
            entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");
            entity.Property(e => e.BorrowedId).HasColumnName("borrowed_id");
            entity.Property(e => e.Charge).HasColumnName("charge");
            entity.Property(e => e.PlannedReturnDate).HasColumnName("planned_return_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .HasColumnName("user");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PRIMARY");

            entity.ToTable("branches");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PRIMARY");

            entity.ToTable("genres");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "login").IsUnique();

            entity.HasIndex(e => e.Mail, "mail").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .HasColumnName("mail");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.UserRole)
                .HasColumnType("enum('admin','user')")
                .HasColumnName("user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
