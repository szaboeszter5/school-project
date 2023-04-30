using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            Console.WriteLine("\n"+entity + " create");

            if (entity == "Actor")
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                rest.Post(new Actor() { ActorName = name}, "actor");
            }
            if (entity == "Role")
            {
                Console.Write("MovieId: ");
                string MovieId = Console.ReadLine();

                Console.Write("ActorId: ");
                string ActorId = Console.ReadLine();

                Console.Write("Priority: ");
                string Priority = Console.ReadLine();

                Console.Write("RoleName: ");
                string RoleName = Console.ReadLine();

                rest.Post(new Role() 
                { 
                    RoleName = RoleName,
                    MovieId = int.Parse(MovieId),
                    ActorId = int.Parse(ActorId),
                    Priority = int.Parse(Priority)
                }, "role");
            }
            if (entity == "Director")
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                rest.Post(new Director() { DirectorName = name }, "director");
            }
            if (entity == "Movie")
            {
                Console.Write("Title: ");
                string Title = Console.ReadLine();

                Console.Write("Income: ");
                string Income = Console.ReadLine();

                Console.Write("DirectorId: ");
                string DirectorId = Console.ReadLine();

                Console.Write("Release: ");
                string Release = Console.ReadLine();

                Console.Write("Rating: ");
                string Rating = Console.ReadLine();

                rest.Post(new Movie()
                { 
                    Title = Title,
                    Income = int.Parse(Income),
                    DirectorId = int.Parse(DirectorId),
                    Release = DateTime.Parse(Release),
                    Rating = int.Parse(Rating)

                }, "movie");
            }

            Console.WriteLine(entity + " created and added.");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Actor")
            {
                Console.WriteLine("Id" + "\t" + "Name");
                List<Actor> actors = rest.Get<Actor>("actor");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.ActorId + "\t" + item.ActorName);
                }
            }
            if (entity == "Role")
            {
                Console.WriteLine("id" + "\t" + "Name");
                List<Role> roles = rest.Get<Role>("role");
                foreach (var item in roles)
                {
                    Console.WriteLine(item.RoleId + "\t" + item.RoleName);
                }
            }
            if (entity == "Director")
            {
                Console.WriteLine("Id" + "\t" + "Name");
                List<Director> directors = rest.Get<Director>("director");
                foreach (var item in directors)
                {
                    Console.WriteLine(item.DirectorId + "\t" + item.DirectorName);
                }


            }
            if (entity == "Movie")
            {
                Console.WriteLine("Id" + "\t" + "Title");
                List<Movie> movies = rest.Get<Movie>("movie");
                foreach (var item in movies)
                {
                    Console.WriteLine(item.MovieId + "\t" + item.Title);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine("\n"+entity + " update");

            if (entity == "Actor")
            {
                Console.Write("Id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("new Name: ");
                string name = Console.ReadLine();

                Actor a = rest.Get<Actor>(id, "actor");
                a.ActorName = name;
                rest.Put(a,"actor");
            }
            if (entity == "Role")
            {
                Console.Write("RoleId: ");
                string RoleId = Console.ReadLine();

                Console.Write("new MovieId: ");
                string MovieId = Console.ReadLine();

                Console.Write("new ActorId: ");
                string ActorId = Console.ReadLine();

                Console.Write("new Priority: ");
                string Priority = Console.ReadLine();

                Console.Write("new RoleName: ");
                string RoleName = Console.ReadLine();

                Role r = rest.Get<Role>(int.Parse(RoleId),"role");
                r.RoleName = RoleName;
                r.Priority = int.Parse(Priority);
                r.ActorId = int.Parse(ActorId);
                r.MovieId = int.Parse(MovieId);
                r.RoleId = int.Parse(RoleId);
                rest.Put(r,"role");
            }
            if (entity == "Director")
            {
                Console.Write("Id: ");
                string id = Console.ReadLine();

                Console.Write("new name: ");
                string name = Console.ReadLine();

                Director d = rest.Get<Director>(int.Parse(id),"director");
                d.DirectorName = name;
                rest.Put(d,"director");
            }
            if (entity == "Movie")
            {
                Console.Write("Id: ");
                string MovieId = Console.ReadLine();

                Console.Write("new Title: ");
                string Title = Console.ReadLine();

                Console.Write("new Income: ");
                string Income = Console.ReadLine();

                Console.Write("new DirectorId: ");
                string DirectorId = Console.ReadLine();

                Console.Write("new Release: ");
                string Release = Console.ReadLine();

                Console.Write("new Rating: ");
                string Rating = Console.ReadLine();

                Movie m = rest.Get<Movie>(int.Parse(MovieId),"movie");
                m.Title = Title;
                m.Income = int.Parse(Income);
                m.DirectorId = int.Parse(DirectorId);
                m.Release = DateTime.Parse(Release);
                m.Rating = int.Parse(Rating);
                rest.Put(m,"movie");
            }
            Console.WriteLine(entity + " updated.");

            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine("\n"+entity + " delete");

            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());

            switch (entity)
            {
                case "Actor": rest.Delete(id,"actor"); break;

                case "Director": rest.Delete(id, "director"); break;

                case "Movie": rest.Delete(id, "movie"); break;

                case "Role": rest.Delete(id, "role"); break;
            }

            Console.WriteLine(entity + " deleted.");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:23125/", "movie");

            var actorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Actor"))
                .Add("Create", () => Create("Actor"))
                .Add("Delete", () => Delete("Actor"))
                .Add("Update", () => Update("Actor"))
                .Add("Exit", ConsoleMenu.Close);

            var roleSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Role"))
                .Add("Create", () => Create("Role"))
                .Add("Delete", () => Delete("Role"))
                .Add("Update", () => Update("Role"))
                .Add("Exit", ConsoleMenu.Close);

            var directorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Director"))
                .Add("Create", () => Create("Director"))
                .Add("Delete", () => Delete("Director"))
                .Add("Update", () => Update("Director"))
                .Add("Exit", ConsoleMenu.Close);

            var movieSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Movie"))
                .Add("Create", () => Create("Movie"))
                .Add("Delete", () => Delete("Movie"))
                .Add("Update", () => Update("Movie"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Movies", () => movieSubMenu.Show())
                .Add("Actors", () => actorSubMenu.Show())
                .Add("Roles", () => roleSubMenu.Show())
                .Add("Directors", () => directorSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
