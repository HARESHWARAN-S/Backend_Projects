# Library Management System - Project Development Guide

## Purpose

This document is not just a README for the Library Management System. It is a step-by-step guide that can be followed to build any ASP.NET Core Web API application from scratch.

Examples:

* Library Management System
* School ERP
* Banking System
* Inventory Management System
* Hospital Management System

The sequence remains largely the same.

---

# Phase 1: Understand the Problem

Before writing code, answer:

1. What is the application?
2. Who are the users?
3. What operations can they perform?
4. What data needs to be stored?

Example: Library Management System

Users:

* Librarian
* Member

Operations:

* Add Books
* Add Members
* Borrow Books
* Return Books
* Pay Fines

Data:

* Books
* Copies
* Members
* Borrows
* Fines

Do NOT start coding immediately.

Always spend time identifying entities and operations.

---

# Phase 2: Design the Entities

Identify all entities.

Example:

Book

* BookId
* Title
* Author
* Category

Copy

* CopyId
* BookId
* Status

Member

* MemberId
* Name
* Email
* Phone
* Status
* Type

Borrow

* BorrowId
* CopyId
* MemberId
* BorrowDate
* DueDate
* ReturnDate
* Status

Fine

* BorrowId
* MemberId
* Amount
* Status

These will later become database tables.

---

# Phase 3: Create the Solution Structure

Create project.

Recommended folders:

Models
DTOs
Repositories
Services
Controllers
Context
Exceptions

Example structure:

Library
|
|-- Models
|-- DTOs
|-- Repositories
|-- Services
|-- Controllers
|-- Context
|-- Exceptions

Keep responsibilities separated.

---

# Phase 4: Create Models

Create entity classes.

Example:

Book.cs

public class Book
{
public string BookId { get; set; }
public string Title { get; set; }
}

Do this for all entities.

At this stage:

NO Controllers
NO Services
NO Database

Only Models.

---

# Phase 5: Configure Database

Install packages:

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Tools

Npgsql.EntityFrameworkCore.PostgreSQL

Create DbContext.

Example:

public class LibraryDbContext : DbContext
{
public DbSet<Book> Books { get; set; }

```
public LibraryDbContext(DbContextOptions options)
    : base(options)
{
}
```

}

Register in Program.cs.

---

# Phase 6: Configure Connection String

appsettings.json

{
"ConnectionStrings":
{
"DefaultConnection":
"Host=localhost;Port=5432;Database=LibraryDB;Username=postgres;Password=yourpassword"
}
}

Register DbContext.

builder.Services.AddDbContext<LibraryDbContext>(
options =>
options.UseNpgsql(
builder.Configuration.GetConnectionString("DefaultConnection")));

Run application once.

Fix connection issues before proceeding.

---

# Phase 7: Create Migrations

Create first migration.

dotnet ef migrations add InitialCreate

Apply migration.

dotnet ef database update

Verify tables exist in PostgreSQL.

Only after this proceed.

---

# Phase 8: Create DTOs

Never expose Models directly through APIs.

Create DTOs.

Example:

BookCreateDto

BookReadDto

BookUpdateDto

Purpose:

Controller -> DTO

Service -> Model

Service -> DTO

Benefits:

* Validation
* Security
* Flexibility

---

# Phase 9: Create Repository Interfaces

Example:

IBookRepository

Methods:

Get

GetAll

Add

Update

Delete

Repository should ONLY interact with database.

No business logic here.

---

# Phase 10: Create Repository Implementations

Use DbContext.

Example:

public List<Book> GetAll()
{
return _context.Books.ToList();
}

public Book Add(Book book)
{
_context.Books.Add(book);
_context.SaveChanges();

```
return book;
```

}

Repository responsibility:

Database only.

---

# Phase 11: Register Dependency Injection

Program.cs

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IMemberRepository, MemberRepository>();

Never manually create repositories using new.

Use dependency injection.

---

# Phase 12: Create Service Layer

Service contains business rules.

Example:

Borrow Book

Checks:

* Member exists
* Member active
* Fine limit
* Copy available
* Borrow limit

Only then:

Create borrow record

Update copy status

Save changes

Repository = Data

Service = Logic

---

# Phase 13: Create Service Interfaces

IBookService

IMemberService

IBorrowService

IFineService

Makes testing and maintenance easier.

---

# Phase 14: Create Controllers

Controllers should be thin.

Example:

[HttpPost]
public IActionResult AddBook(BookCreateDto dto)
{
return Ok(_bookService.AddBook(dto));
}

Controller should NOT contain business logic.

---

# Phase 15: Add Exception Handling

Create custom exceptions.

Examples:

BookNotFoundException

MemberNotFoundException

BorrowLimitReachedException

AlreadyBorrowedException

Use exceptions instead of returning random strings.

---

# Phase 16: Test Using Swagger

Test every API.

Example flow:

1. Add Member
2. Add Book
3. Add Copy
4. Borrow Book
5. Return Book
6. Pay Fine

Verify database after every operation.

---

# Phase 17: Verify Database

Never trust Swagger response alone.

Check PostgreSQL tables.

Verify:

Books

Copies

Members

Borrows

Fines

Ensure data is actually saved.

---

# Phase 18: Refactoring

Once application works:

Remove duplicate code.

Create mapping helpers.

Improve naming.

Add validation.

Optimize queries.

Do NOT optimize before functionality works.

---

# Common Mistakes and How to Prevent Them

## Mistake 1

Using List<T> instead of DbContext

Problem:

Data disappears after application restart.

Fix:

Use EF Core and DbContext.

---

## Mistake 2

Fetching data once in constructor

Example:

memberlist = repository.GetAll();

Problem:

Data becomes stale.

Fix:

Call repository.GetAll() inside methods.

---

## Mistake 3

Forgetting SaveChanges()

Problem:

Data appears updated in memory but not in database.

Fix:

Call SaveChanges() after Add/Update/Delete.

---

## Mistake 4

Updating object but not calling Update()

Problem:

Changes not persisted.

Fix:

_context.Entity.Update(entity);

_context.SaveChanges();

---

## Mistake 5

Using DateTime.Now with PostgreSQL timestamptz

Problem:

UTC conversion errors.

Fix:

Use DateTime.UtcNow.

---

## Mistake 6

Returning Models directly

Problem:

Tight coupling.

Fix:

Return DTOs.

---

## Mistake 7

Putting business logic inside Controllers

Problem:

Hard to maintain.

Fix:

Move logic to Services.

---

## Mistake 8

Creating repository objects manually

Problem:

Dependency Injection breaks.

Fix:

Register services in Program.cs.

Use constructor injection.

---

## Mistake 9

Changing entity classes but forgetting migrations

Problem:

Database schema mismatch.

Fix:

dotnet ef migrations add MigrationName

dotnet ef database update

---

## Mistake 10

Not checking database after API calls

Problem:

Swagger says success but data not saved.

Fix:

Verify tables directly in PostgreSQL.

---

# Recommended Development Order

1. Requirement Analysis
2. Entity Design
3. Models
4. DbContext
5. Database Connection
6. Migrations
7. DTOs
8. Repository Interfaces
9. Repository Implementations
10. Dependency Injection
11. Service Interfaces
12. Services
13. Controllers
14. Exception Handling
15. Swagger Testing
16. Database Verification
17. Refactoring
18. Documentation

Follow this order for every project.
