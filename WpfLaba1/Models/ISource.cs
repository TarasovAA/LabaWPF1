using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfLaba1.Models
{
    public interface ISource: INotifyPropertyChanged
    {
        int Count { get; }
        void SaveChanges();
        void Dispose();
        bool Remove(Hero hero);
        bool Add(Hero hero);
        ReadOnlyObservableCollection<Hero> HeroesList { get; }
        void onPropertyChanged(string prop = "");
    }
}
