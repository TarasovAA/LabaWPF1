using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfLaba1.Models
{
    public interface ISource
    {
        int Count { get; }
        void SaveChanges();
        void Dispose();
        bool Change(Hero hero);
        bool Remove(Hero hero);
        bool Add(Hero hero);
        ReadOnlyObservableCollection<Hero> HeroesList { get; }
    }
}
