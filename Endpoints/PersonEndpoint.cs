using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RestAPICVHantering.Data;
using RestAPICVHantering.Models;

public static class PersonEndpoint
{
    public static void MapPersonEndpoints(this WebApplication app)
    {
        // Get all Persons with their relationships
        app.MapGet("/persons", async (APICVDBContext db) =>
        {
            var persons = await db.Persons
                .Include(p => p.Education)     
                .Include(p => p.WorkExperience)   
                .ToListAsync();

            return Results.Ok(persons);
        });

        // Get a Person by ID
        app.MapGet("/persons/{id}", async (APICVDBContext db, int id) =>
        {
            var person = await db.Persons.FindAsync(id);
            return person != null ? Results.Ok(person) : Results.NotFound();
        });

        // Create a new Person
        app.MapPost("/persons", async (APICVDBContext db, Person person) =>
        {
            // Remove manual ID assignments
            person.PersonID = 0;
            person.Education?.ForEach(e => e.EducationID = 0);
            person.WorkExperience?.ForEach(w => w.ExperienceID = 0);

            using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                db.Persons.Add(person);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Results.Created($"/persons/{person.PersonID}", person);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return Results.Problem($"Error: {ex.Message}");
            }
        });

        // Update an existing Person
        app.MapPut("/persons/{id}", async (APICVDBContext db, int id, Person updatedPerson) =>
        {
            // Validation
            if (!IsPersonValid(updatedPerson, out var errors))
                return Results.ValidationProblem(errors);

            var person = await db.Persons.FindAsync(id);
            if (person == null)
                return Results.NotFound();

            try
            {
                person.FirstName = updatedPerson.FirstName;
                person.LastName = updatedPerson.LastName;
                person.Email = updatedPerson.Email;
                person.Phone = updatedPerson.Phone;

                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            catch
            {
                return Results.Problem("Could not update person");
            }
        });

        // Delete a Person
        app.MapDelete("/persons/{id}", async (APICVDBContext db, int id) =>
        {
            var person = await db.Persons
                .Include(p => p.Education)
                .Include(p => p.WorkExperience)
                .FirstOrDefaultAsync(p => p.PersonID == id);

            if (person == null)
                return Results.NotFound();

            try
            {
                db.Educations.RemoveRange(person.Education);
                db.WorkExperiences.RemoveRange(person.WorkExperience);
                db.Persons.Remove(person);

                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            catch
            {
                return Results.Problem("Could not delete person");
            }
        });
    }

    private static bool IsPersonValid(Person person, out IDictionary<string, string[]> errors)
    {
        errors = new Dictionary<string, string[]>();
        var isValid = true;

        if (string.IsNullOrWhiteSpace(person.FirstName))
        {
            errors.Add("FirstName", new[] { "First name is required" });
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(person.LastName))
        {
            errors.Add("LastName", new[] { "Last name is required" });
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(person.Email) || !person.Email.Contains('@'))
        {
            errors.Add("Email", new[] { "Valid email is required" });
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(person.Phone))
        {
            errors.Add("Phone", new[] { "Phone number is required" });
            isValid = false;
        }

        return isValid;
    }
}
