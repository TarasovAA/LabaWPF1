using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WpfLaba1.Models
{
    public class MongoSource : ISource
    {
        public int Count => throw new NotImplementedException();
        IMongoCollection<Hero> collection;

        public MongoSource()
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase("HeroesMongoDB");
            collection = database.GetCollection<Hero>("Heroes");
            heroesList = new ObservableCollection<Hero>(collection.Find((_ => true)).ToList<Hero>());
            HeroesList = new ReadOnlyObservableCollection<Hero>(heroesList);
        }
        public ReadOnlyObservableCollection<Hero> HeroesList { get; private set; }
        private ObservableCollection<Hero> heroesList;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Add(Hero hero)
        {
            heroesList.Add(hero);
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void onPropertyChanged(string prop = "")
        {
            throw new NotImplementedException();
        }

        public bool Remove(Hero hero)
        {
            heroesList.Remove(hero);
            return false;
        }

        public void SaveChanges()
        {
            
        }

        public bool Change(Hero hero)
        {
            throw new NotImplementedException();
        }
    }
}
