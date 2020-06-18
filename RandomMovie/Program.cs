using Microsoft.EntityFrameworkCore;
using RandomMovie.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace RandomMovie
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }

        private static void ShowMenu() // Show the App Menu
        {
            Console.WriteLine("RANDOM MOVIE PICKER");
            Console.WriteLine("DEVOLPED BY YASSER PULIDO");
            Console.WriteLine("");
            Console.WriteLine("1. Create.");
            Console.WriteLine("2. Read.");
            Console.WriteLine("3. Update.");
            Console.WriteLine("4. Delete.");
            Console.WriteLine("5. Pick.");
            Console.WriteLine("6. Exit.");
            Console.WriteLine("");

            int optionMenu = -1;
            bool validateMenu = true;
            while (validateMenu)
            {
                Console.Write("Type an option: ");

                while (!int.TryParse(Console.ReadLine(), out optionMenu))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                }

                if (optionMenu >= 1 && optionMenu <= 6)
                {
                    validateMenu = false;
                }

                else
                {
                    Console.Write("Please enter an integer value between 1-6: ");
                    validateMenu = true;
                }
            }

            switch (optionMenu)
            {
                case 1:
                    CreateMovie();
                    break;
                case 2:
                    ReadMovie();
                    break;
                case 3:
                    UpdateMenu();
                    break;
                case 4:
                    DeleteMovie();
                    break;
                case 5:
                    PickMovie();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }

        private static void CreateMovie() // Create a new record.
        {
            Console.WriteLine("");
            Console.WriteLine("Type the following data to create the movie file:");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;

            using (var context = new RDBContext())
            {
                var newMovie = new Tmovie();

                Console.Write("Title: ");
                newMovie.Title = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().Trim());
                Console.Write("Year: ");
                newMovie.Year = int.Parse(Console.ReadLine().Trim());

                var existingMovie = context.Tmovie.Where(x => x.Title.ToLower() == newMovie.Title.ToLower()).FirstOrDefault();

                Console.WriteLine("");

                if (existingMovie == null)
                {
                    context.Tmovie.Add(newMovie);
                    var result = context.SaveChanges();

                    if (result == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The movie was created succesfully.");
                        Console.ForegroundColor = ConsoleColor.Gray;

                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error, the movie wasn't created.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error, already existed a movie with the same name.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Console.WriteLine("");
            Console.Write("Type any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
        }

        private static void ReadMovie() // List all records registered.
        {
            Console.WriteLine("");

            Console.WriteLine("1. All.");
            Console.WriteLine("2. Picked.");
            Console.WriteLine("3. Exit.");
            Console.WriteLine("");

            int optionMenu = -1;
            bool validateMenu = true;
            while (validateMenu)
            {
                Console.Write("Type an option: ");

                while (!int.TryParse(Console.ReadLine(), out optionMenu))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                }

                if (optionMenu >= 1 && optionMenu <= 3)
                {
                    validateMenu = false;
                }

                else
                {
                    Console.Write("Please enter an integer value between 1-3: ");
                    validateMenu = true;
                }
            }

            Console.WriteLine("");

            switch (optionMenu)
            {
                case 1:
                    using (var context = new RDBContext())
                    {
                        var listMovie = context.Tmovie.ToList();

                        if (listMovie != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            foreach (var movie in listMovie)
                            {
                                Console.WriteLine("ID: " + movie.Idmovie + ". Title: " + movie.Title + ". Year: " + movie.Year + ".");
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("It doesn't exists movies to read.");
                        }
                    }
                    break;
                case 2:
                    using (var context = new RDBContext())
                    {
                        var listMovie = context.Tmovie.Where(x => x.Pick == true).OrderBy(x => x.PickDate).ToList();

                        if (listMovie != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            foreach (var movie in listMovie)
                            {
                                Console.WriteLine("ID: " + movie.Idmovie + ". Title: " + movie.Title + ". Year: " + movie.Year + ".");
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("It doesn't exists movies to read.");
                        }
                    }
                    break;
                case 3:
                    ShowMenu();
                    break;
                default:
                    break;
            }

            Console.WriteLine("");
            Console.Write("Type any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
        }

        private static void UpdateMenu() // Update a existing record.
        {
            Console.WriteLine("");
            Console.Write("Type a id movie that you want to update: ");
            int idMovie = int.Parse(Console.ReadLine());
            Console.WriteLine("");

            using (var context = new RDBContext())
            {
                var updateMovie = context.Tmovie.Where(x => x.Idmovie == idMovie).FirstOrDefault();

                if (updateMovie != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1.Title: " + updateMovie.Title);
                    Console.WriteLine("2.Year: " + updateMovie.Year);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("");
                    Console.Write("What data do you want to update? ");
                    int dataOption = int.Parse(Console.ReadLine());
                    Console.WriteLine("");

                    switch (dataOption)
                    {
                        case 1:
                            do
                            {
                                Console.Write("Type a new Title: ");
                                updateMovie.Title = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine()).Trim();
                            } while (updateMovie.Title == "");

                            break;
                        case 2:
                            do
                            {
                                Console.Write("Type a new Year: ");
                                updateMovie.Title = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine()).Trim();
                            } while (updateMovie.Title == "");

                            break;
                        default:
                            break;
                    }

                    var existingMovie = context.Tmovie.Where(x => x.Title.ToLower() == updateMovie.Title.ToLower()).FirstOrDefault();

                    Console.WriteLine("");

                    if (existingMovie == null)
                    {
                        context.Tmovie.Update(updateMovie);
                        var result = context.SaveChanges();

                        if (result == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The movie was updated succesfully.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error, the movie wasn't updated.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error, already existed a movie with the same name.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("It doesn't exist any movie with the id typed.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Console.WriteLine("");
            Console.Write("Type any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
        }

        private static void DeleteMovie()
        {
            Console.WriteLine("");
            Console.Write("Type a id movie that you want to delete: ");
            int idMovie = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            using (var context = new RDBContext())
            {
                var deleteMovie = context.Tmovie.Where(x => x.Idmovie == idMovie).FirstOrDefault();

                if (deleteMovie != null)
                {
                    Console.Write("Do you want to delete ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(deleteMovie.Title);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("?");
                    Console.WriteLine("");

                    char deleteOption = '\0';
                    bool optionToggle = true;
                    do
                    {
                        Console.Write("Type an option (S/N): ");
                        deleteOption = Console.ReadKey().KeyChar;

                        if (deleteOption == 's' || deleteOption == 'S' || deleteOption == 'n' || deleteOption == 'N')
                        {
                            optionToggle = false;
                        }
                    } while (optionToggle);

                    if (deleteOption == 's' || deleteOption == 'S')
                    {
                        Console.WriteLine("");

                        context.Tmovie.Remove(deleteMovie);
                        var result = context.SaveChanges();

                        if (result == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The movie was deleted succesfully.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error, the movie wasn't deleted.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("It doesn't exist any movie with the id typed.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine("");
                Console.Write("Type any key to continue...");
                Console.ReadKey();
                Console.Clear();
                ShowMenu();
            }
        }
        private static void PickMovie()
        {
            using (var context = new RDBContext())
            {
                var random = new Random();

                int listMovie = context.Tmovie.ToList().Count;

                int randomNumber = random.Next(0, listMovie);

                Console.WriteLine("");
                Console.Write("The movie picked was: ");

                var moviePicked = context.Tmovie.Where(x => x.Idmovie == randomNumber && x.Pick == false).FirstOrDefault();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ID: " + moviePicked.Idmovie + ". Title: " + moviePicked.Title + ". Year: " + moviePicked.Year + ".");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");

                moviePicked.Pick = true;
                moviePicked.PickDate = DateTime.Now.Date;

                context.Tmovie.Update(moviePicked);
                var result = context.SaveChanges();

                if (result == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The movie was updated succesfully.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error, the movie wasn't updated.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine("");
                Console.Write("Type any key to continue...");
                Console.ReadKey();
                Console.Clear();
                ShowMenu();
            }
        }
    }
}
