using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConsoleApp1
{
    public class Program
    {
        static IMongoClient client;
        static IMongoDatabase db;
        static void Main(string[] args)
        {
            BasicQuery();
        }
        private static void BasicQuery()
        {
            client = new MongoClient();
            db = client.GetDatabase("ITRI617ResearchDemo");
            var coll = db.GetCollection<Customer>("Customer");

            var americanCustomers = coll.Find(c => c.CompanyName == "Seven Seas Imports").ToListAsync().Result;
            string title = "Customers from United States";
            Console.WriteLine(title);
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", title.Length)));
            foreach (var c in americanCustomers)
            {
                Console.WriteLine($"Name: {c.ContactName}, \t City: {c.City} ");
            }
            Console.Read();
        }
        
    }
}
