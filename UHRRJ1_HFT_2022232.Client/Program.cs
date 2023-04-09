using ConsoleTools;
using System;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Client
{
    internal class Program
    {
        static ActorLogic actorLogic;
        static RoleLogic roleLogic;
        static DirectorLogic directorLogic;
        static MovieLogic movieLogic;

        static void Create(string entity)
        {
            Console.WriteLine("\n"+entity + " create");

            if (entity == "Actor")
            {
                Console.Write("Id: ");
                string id = Console.ReadLine();

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Actor a = new Actor(id+"#"+name);
                actorLogic.Create(a);
            }
            if (entity == "Role")
            {
                Console.Write("RoleId: ");
                string RoleId = Console.ReadLine();

                Console.Write("MovieId: ");
                string MovieId = Console.ReadLine();

                Console.Write("ActorId: ");
                string ActorId = Console.ReadLine();

                Console.Write("Priority: ");
                string Priority = Console.ReadLine();

                Console.Write("RoleName: ");
                string RoleName = Console.ReadLine();

                Role r = new Role($"{RoleId}#{MovieId}#{ActorId}#{Priority}#{RoleName}");
                roleLogic.Create(r);
            }
            if (entity == "Director")
            {
                Console.Write("Id: ");
                string id = Console.ReadLine();

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Director d = new Director(id + "#" + name);
                directorLogic.Create(d);
            }
            if (entity == "Movie")
            {
                Console.Write("Id: ");
                string MovieId = Console.ReadLine();

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

                Movie m = new Movie($"{MovieId}#{Title}#{Income}#{DirectorId}#{Release}#{Rating}");
                movieLogic.Create(m);
            }

            Console.WriteLine(entity + " created and added.");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Actor")
            {
                var items = actorLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.ActorId + "\t" + item.ActorName);
                }
            }
            if (entity == "Role")
            {
                var items = roleLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.RoleId + "\t" + item.RoleName);
                }
            }
            if (entity == "Director")
            {
                var items = directorLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.DirectorId + "\t" + item.DirectorName);
                }
            }
            if (entity == "Movie")
            {
                var items = movieLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
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
                string id = Console.ReadLine();

                Console.Write("new Name: ");
                string name = Console.ReadLine();

                Actor a = new Actor(id + "#" + name);
                actorLogic.Update(a);
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

                Role r = new Role($"{RoleId}#{MovieId}#{ActorId}#{Priority}#{RoleName}");
                roleLogic.Update(r);
            }
            if (entity == "Director")
            {
                Console.Write("Id: ");
                string id = Console.ReadLine();

                Console.Write("new name: ");
                string name = Console.ReadLine();

                Director d = new Director(id + "#" + name);
                directorLogic.Update(d);
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

                Movie m = new Movie($"{MovieId}#{Title}#{Income}#{DirectorId}#{Release}#{Rating}");
                movieLogic.Update(m);
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
                case "Actor": actorLogic.Delete(id); break;

                case "Director": directorLogic.Delete(id); break;

                case "Movie": movieLogic.Delete(id); break;

                case "Role": roleLogic.Delete(id); break;
            }

            Console.WriteLine(entity + " deleted.");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var ctx = new MovieDbContext();

            var movieRepo = new MovieRepository(ctx);
            var roleRepo = new RoleRepository(ctx);
            var actorRepo = new ActorRepository(ctx);
            var directorRepo = new DirectorRepository(ctx);

            movieLogic = new MovieLogic(movieRepo);
            roleLogic = new RoleLogic(roleRepo);
            actorLogic = new ActorLogic(actorRepo);
            directorLogic = new DirectorLogic(directorRepo);

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
