using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WpfLaba1.Models
{
    public class JSONRecords:ISource
    {
        public int Count => throw new NotImplementedException();

        public ReadOnlyObservableCollection<Hero> HeroesList => throw new NotImplementedException();
        private ObservableCollection<Hero> heroList;
       
        public JSONRecords()
        {
            heroList = new ObservableCollection<Hero>();
           
            string fileContent = File.ReadAllText(@"C:\Users\taras\source\repos\WpfLaba1\WpfLaba1\jsonSource.json");
            List<Hero> listJson = JsonConvert.DeserializeObject<List<Hero>>(fileContent);
        }

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
