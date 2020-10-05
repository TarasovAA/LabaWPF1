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

        public MongoSource()
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            
        }
        public ReadOnlyObservableCollection<Hero> HeroesList => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Add(Hero hero)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
