using System;
using System.Collections;
using System.Collections.Generic;
namespace Standart_Interfaces__Enum
{
    class Cinema : IEnumerable
    {
        private List<Movie> movies = new List<Movie>();
        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }
        public void SortMovies(IComparer<Movie> comparer)
        {
            movies.Sort(comparer);
        }
        public IEnumerator GetEnumerator()
        {
            return movies.GetEnumerator();
        }
    }
    class Director : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Director()
        {
            FirstName = "None first name";
            LastName = "None last name";
            Description = "None description";
        }
        public Director(string F, string L, string d)
        {
            FirstName = F;
            LastName = L;
            Description = d;
        }
        public override string ToString()
        {
            return $"First name: {FirstName}, Last name: {LastName}, Description: {Description}";
        }
        public object Clone()
        {
            return new Director(FirstName, LastName, Description);
        }
    }
    enum Genre {Fantasy, Comedy, Horror, Adventure, Dramma }
    class Movie : ICloneable, IComparable
    {
        public string Title { get; set; }
        public Director Director { get; set; }
        public string Country { get; set; }
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public byte Rating { get; set; }
        public object Clone()
        {
            return new Movie
            {
                Title = this.Title,
                Director = (Director)this.Director.Clone(),
                Country = this.Country,
                Genre = this.Genre,
                Year = this.Year,
                Rating = this.Rating
            };
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Movie otherMovie = obj as Movie;
            if (otherMovie != null)
                return this.Title.CompareTo(otherMovie.Title);
            else
                throw new ArgumentException("Object is not a Movie");
        }
        public override string ToString()
        {
            return $"Title: {Title}, Director: {Director}, Country: {Country}, Genre: {Genre}, Year: {Year}, Rating: {Rating}";
        }
    }
    class MovieComparerByYear : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            return x.Year.CompareTo(y.Year);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Cinema cinema = new Cinema();
            Director director1 = new Director("Jama", "Juma", "director1");
            Director director2 = new Director("Gama", "Guma", "director2");
            Movie movie1 = new Movie
            {
                Title = "The First Movie",
                Director = director1,
                Country = "UK",
                Genre = Genre.Horror,
                Year = 2002,
                Rating = 3
            };
            Movie movie2 = new Movie
            {
                Title = "The second Movie",
                Director = director2,
                Country = "USA",
                Genre = Genre.Fantasy,
                Year = 2024,
                Rating = 5
            };
            cinema.AddMovie(movie1);
            cinema.AddMovie(movie2);
            Console.WriteLine("Movies before sorting : ");
            foreach (Movie movie in cinema)
            {
                Console.WriteLine(movie);
            }
            cinema.SortMovies(new MovieComparerByYear());
            Console.WriteLine("\nMovies after sorting by year : ");
            foreach (Movie movie in cinema)
            {
                Console.WriteLine(movie);
            }
        }
    }
}