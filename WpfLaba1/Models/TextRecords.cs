﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfLaba1.Models
{
    public class TextRecords : ISource
    {
        public int Count => throw new NotImplementedException();

        public ReadOnlyObservableCollection<Hero> HeroesList => throw new NotImplementedException();
        private ObservableCollection<Hero> heroList;
        string path = @"C:\Users\taras\source\repos\WpfLaba1\WpfLaba1";

        public TextRecords()
        {
            heroList = new ObservableCollection<Hero>();
            using (FileStream fs = new FileStream($"{path}/FileSource.txt", FileMode.OpenOrCreate))
            {
                
            }
        }

        public override string ToString()
        {
            return "TextRecords";
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