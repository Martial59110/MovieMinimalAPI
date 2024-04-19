
using MySqlConnector;

namespace MovieMinimalAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.MapGet("/movies/{id}", GetMovieById);
        app.MapGet("/movies", GetAllMovie);
        app.MapPost("/movies", AddMovie);
        app.MapPut("/movies/{id}", UpdateMovie);
        app.MapDelete("/movies/{id}", DeleteMovie);
        app.MapGet("/actors/{id}", GetActorById);
        app.MapGet("/actors", GetAllActors);
        app.MapPost("/actors", AddActor);
        app.MapDelete("/actors/{id}", DeleteActor);
        app.Run();
    }

    private static Movie[] GetAllMovie()
    {
        List<Movie> movies = new();



        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("SELECT * FROM movies;", connection);
        using var reader = command.ExecuteReader();
        {
            while (reader.Read())
            {
                Movie movieToAdd = new()
                {
                    Id = (int)reader["movie_id"],
                    Title = reader["movie_title"].ToString(),
                    ReleaseYear = reader["movie_release_year"].ToString(),
                    CreateDate = (DateTime)reader["create_date"],
                };
                movies.Add(movieToAdd);
            }
        }

        return movies.ToArray();
    }

 
    private static Movie GetMovieById(int id)
    {
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("SELECT * FROM movies WHERE movie_id = @id;", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Movie
            {
                Id = (int)reader["movie_id"],
                Title = reader["movie_title"].ToString(),
                ReleaseYear = reader["movie_release_year"].ToString(),
                CreateDate = (DateTime)reader["create_date"],
            };
        }

        return null;
    }

    private static void AddMovie(Movie movie)
    {
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("INSERT INTO movies (movie_title, movie_release_year, create_date) VALUES (@title, @releaseYear, @createDate);", connection);
        command.Parameters.AddWithValue("@title", movie.Title);
        command.Parameters.AddWithValue("@releaseYear", movie.ReleaseYear);
        command.Parameters.AddWithValue("@createDate", DateTime.Now);

        command.ExecuteNonQuery();
    }

    private static void UpdateMovie(int id, Movie updatedMovie)
    {
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("UPDATE movies SET movie_title = @title, movie_release_year = @releaseYear WHERE movie_id = @id;", connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@title", updatedMovie.Title);
        command.Parameters.AddWithValue("@releaseYear", updatedMovie.ReleaseYear);

        command.ExecuteNonQuery();
    }

    private static void DeleteMovie(int id)
    {
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("DELETE FROM movies WHERE movie_id = @id;", connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }

    private static Actor GetActorById(int id)
    {
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("SELECT * FROM actors WHERE actor_id = @id;", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Actor
            {
                Id = (int)reader["actor_id"],
                LastName = reader["actor_name"].ToString(),
                FirstName = reader["actor_first_name"].ToString(),
                BirthDate = (DateTime)reader["actor_birthdate"],
                CreateDate = (DateTime)reader["create_date"],
                ModificationDate = (DateTime)reader["modification_date"],
            };
        }

        return null;
    }
    private static Actor[] GetAllActors()
    {
        List<Actor> actors = new();
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();

        using var command = new MySqlCommand("SELECT * FROM actors;", connection);
        using var reader = command.ExecuteReader();
        {
            while (reader.Read())
            {
                Actor actorsToAdd = new()
                {
                    Id = (int)reader["actor_id"],
                    LastName = reader["actor_name"].ToString(),
                    FirstName = reader["actor_first_name"].ToString(),
                    BirthDate = (DateTime)reader["actor_birthdate"],
                    CreateDate = (DateTime)reader["create_date"],
                    ModificationDate = (DateTime)reader["modification_date"],
                };
                actors.Add(actorsToAdd);
            }
        }
        return actors.ToArray();
    }

private static void AddActor(Actor actor)
{
    using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
    connection.Open();

    
     var birthYearString = actor.BirthDate.ToString("yyyy");

    using var command = new MySqlCommand("INSERT INTO actors (actor_name, actor_first_name, actor_birthdate, create_date, modification_date) VALUES (@lastName, @firstName, @birthDate, @createDate, @modificationDate);", connection);
    command.Parameters.AddWithValue("@lastName", actor.LastName);
    command.Parameters.AddWithValue("@firstName", actor.FirstName);
    command.Parameters.AddWithValue("@birthDate", birthYearString);
    command.Parameters.AddWithValue("@createDate", DateTime.Now);
    command.Parameters.AddWithValue("@modificationDate", DateTime.Now);

    command.ExecuteNonQuery();
}


    private static void DeleteActor(int id)
    {
        using var connection = new MySqlConnection("Server=127.0.0.1;User ID=root;Password=;Database=leana");
        connection.Open();
        using var command = new MySqlCommand("DELETE FROM actors WHERE actor_id = @id;", connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}



