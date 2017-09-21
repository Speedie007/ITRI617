using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {

        internal class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Class { get; set; }
            public int Age { get; set; }
            public IEnumerable<string> Subjects { get; set; }
        }
        static void Main(string[] args)
        {
            MainAsync().Wait();

            Console.ReadLine();
        }
        static async Task MainAsync()
        {
            var client = new MongoClient();

            IMongoDatabase db = client.GetDatabase("school");

            var collection = db.GetCollection<BsonDocument>("students");

            using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<BsonDocument> batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        Console.WriteLine(document);
                        Console.WriteLine();
                    }
                }
            }
            //var client = new MongoClient();

            //IMongoDatabase db = client.GetDatabase("schoool");

            //var collection = db.GetCollection<Student>("students");
            //var newStudents = CreateNewStudents();

            //await collection.InsertManyAsync(newStudents);
        }

        private static IEnumerable<Student> CreateNewStudents()
        {
            var student1 = new Student
            {
                FirstName = "Gregor",
                LastName = "Felix",
                Subjects = new List<string>() { "English", "Mathematics", "Physics", "Biology" },
                Class = "JSS 3",
                Age = 23
            };

            var student2 = new Student
            {
                FirstName = "Machiko",
                LastName = "Elkberg",
                Subjects = new List<string> { "English", "Mathematics", "Spanish" },
                Class = "JSS 3",
                Age = 23
            };

            var student3 = new Student
            {
                FirstName = "Julie",
                LastName = "Sandal",
                Subjects = new List<string> { "English", "Mathematics", "Physics", "Chemistry" },
                Class = "JSS 1",
                Age = 25
            };

            var newStudents = new List<Student> { student1, student2, student3 };

            return newStudents;
        }
    }
}
