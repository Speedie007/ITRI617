using MongoDB.Driver;
using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using MongoDB.Bson;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static IMongoClient client;
        static IMongoDatabase db;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainAsync().Wait();
        }
        public  async Task MainAsync()
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
                        richTextBox1.Text += document.AsString + "\n";
                    }
                }
            }
        }

    }
}
