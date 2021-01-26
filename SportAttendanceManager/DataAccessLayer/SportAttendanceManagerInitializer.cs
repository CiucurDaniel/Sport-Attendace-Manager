using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportAttendanceSystem.Models;

namespace SportAttendanceSystem.DataAccessLayer
{
    public class SportAttendanceManagerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SportAttendanceManagerContext>
    {
        // Seed method method takes the database context object as an input parameter,
        // and the code in the method uses that object to add new entities to the database.
        // For each entity type, the code creates a collection of new entities,
        // adds them to the appropriate DbSet property, and then saves the changes to the database
        protected override void Seed(SportAttendanceManagerContext context)
        {
            //base.Seed(context);

            var dani = new User
            { IdUser = 1, Email = "dani@gmail.com", FirstName = "Daniel", LastName = "Ciucur", Password = "Dani1234" };

            var iovescu = new User
            { IdUser = 2, Email = "iovescu@gmail.com", FirstName = "Andrei", LastName = "Iovescu", Password = "Iove1234" };

            var adi = new User
            { IdUser = 3, Email = "adi@gmail.com", FirstName = "Mike", LastName = "Andrew", Password = "Adi1234" };


            // Add some teachers

            var users = new List<User>
            {
                dani,
                iovescu,
                adi
            };

            users.ForEach(m => context.Users.Add(m));
            context.SaveChanges();

            // Add some sports

            var basket = new Sport
            {
                IdSport = 3,
                IdUser = 1,
                SportName = "Basket",
                DayOfWeek = "Sunday",
                Hour = 11,
                Minute = 15,
                Description = "Basket la sala Olimpia"
            };

            var fotbal = new Sport
            {
                IdSport = 2,
                IdUser = 2,
                SportName = "Fotbal",
                DayOfWeek = "Monday",
                Hour = 8,
                Minute = 20,
                Description = "Basket la sala Olimpia"
            };

            var inot = new Sport
            {
                IdSport = 1,
                IdUser = 3,
                SportName = "Inot",
                DayOfWeek = "Friday",
                Hour = 10,
                Minute = 40,
                Description = "Inot la bazinul Dacia langa Iulius Mall"
            };

            var sports = new List<Sport>
            {
                basket,
                inot,
                fotbal
            };

            sports.ForEach(sport => context.Sports.Add(sport));
            context.SaveChanges();

            // Add some students 

            var students = new List<Student>
            {
                // 3 students who are enrolled at Basket
                new Student{ IdSport = 3, IdStudent = 1, FirstName = "Andrei", LastName = "Iovescu", Email = "andrei.iovescu99@e-uvt.ro"},
                new Student{ IdSport = 3, IdStudent = 2, FirstName = "Alexandru", LastName = "Borza", Email = "alexandru.borza99@e-uvt.ro"},
                new Student{ IdSport = 3, IdStudent = 3, FirstName = "Carmen", LastName = "Popescu", Email = "carmen.popescu@e-uvt.ro"},

                // 3 students who are enrolled at football
                new Student{ IdSport = 2, IdStudent = 4, FirstName = "Opra", LastName = "Andrei", Email = "andrei.opra99@e-uvt.ro"},
                new Student{ IdSport = 2, IdStudent = 5, FirstName = "Alexandru", LastName = "Borza", Email = "alexandru.borza99@e-uvt.ro"},
                new Student{ IdSport = 2, IdStudent = 6, FirstName = "Carmen", LastName = "Popescu", Email = "carmen.popescu@e-uvt.ro"},

                // 3 students who are enrolled at swimming
                new Student{ IdSport = 1, IdStudent = 7, FirstName = "Mihai", LastName = "Andrei", Email = "andrei.mihai99@e-uvt.ro"},
                new Student{ IdSport = 1, IdStudent = 8, FirstName = "Zeke", LastName = "Manuel", Email = "zeke.manuel99@e-uvt.ro"},
                new Student{ IdSport = 1, IdStudent = 9, FirstName = "Carmen", LastName = "Popescu", Email = "carmen.popescu@e-uvt.ro"}
            };

            students.ForEach(student => context.Students.Add(student));
            context.SaveChanges();

            // Next step:
            // To tell Entity Framework to use your initializer class,
            // add an element to the entityFramework element in the application Web.config file
            // (the one in the root project folder)

        }
    }
}