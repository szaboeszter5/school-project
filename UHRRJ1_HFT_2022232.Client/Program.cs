using ConsoleTools;
using System;
using UHRRJ1_HFT_2022232.Logic;
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
            Console.WriteLine(entity + " create");
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
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }

        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }

        // LOGIC MŰVELETEK IMPLEMENTÁLÁSA

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
