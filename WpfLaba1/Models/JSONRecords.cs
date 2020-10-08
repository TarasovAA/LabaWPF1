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
    public class JSONRecords:ISource // модель для источника в формате файла json
    {
        public int Count => heroList.Count;
       
        public ReadOnlyObservableCollection<Hero> HeroesList { get; private set; }
        private ObservableCollection<Hero> heroList;
        private string path;

        public JSONRecords()
        {
            heroList = new ObservableCollection<Hero>();
            path = Directory.GetCurrentDirectory()+"/jsonSource.json";
            string fileContent = null;
            if (File.Exists(path))
            {
                fileContent = File.ReadAllText(path);
            }
            else
            {
                File.Create(path);
            }

            if (!string.IsNullOrEmpty(fileContent))
            {
                heroList = JsonConvert.DeserializeObject<ObservableCollection<Hero>>(fileContent);
                HeroesList = new ReadOnlyObservableCollection<Hero>(heroList);
            }
            else
            {
                heroList = new ObservableCollection<Hero>();
                HeroesList = new ReadOnlyObservableCollection<Hero>(heroList);
            }
        }

        public override string ToString()
        {
            return "JSONRecords";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Add(Hero hero)
        {
            heroList.Add(hero);
            return true;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void onPropertyChanged(string prop = "")
        {
            throw new NotImplementedException();
        }

        public bool Remove(Hero hero)
        {
            heroList.Remove(hero);
            return false;
        }

        public void SaveChanges()
        {
            string json = JsonConvert.SerializeObject(heroList, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public bool Change(Hero hero)
        {
            throw new NotImplementedException();
        }
    }
}
