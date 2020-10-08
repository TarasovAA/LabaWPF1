using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLaba1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Data.Entity;
using WpfLaba1.View;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WpfLaba1.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        //Hero selectedHero;
        //Главная view-модель
        ISource source; // выбранный на данный момент источник данных
        public ISource Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                onPropertyChanged("SelectedSource");
                onPropertyChanged("HeroesList"); //обговляет отоброжения списка
            }
        }
        public List<ISource> DataSources { get; set; } // Список всех источников данных ( на данный момент реализованы база данных из mssql сервера и источник данных, списанный из json файла)

        

        public ReadOnlyObservableCollection<Hero> HeroesList => source.HeroesList; //пробросс к списку данных из источника

        // нужно для реализации множественной копипасты из одного источника данных в другой
        List<Hero> insertHeroes; // вырезанные элементы

        //Hero ChabgingHero;

        IList selectedHeroes; // нужно для реализации множественной копипасты из одного источника данных в другой

        public IList SelectedHeroes // собственно считыные с помощью биндинга элементы
        {
            get
            {
                return selectedHeroes;
            }
            set
            {
                selectedHeroes = value;
                onPropertyChanged("SelectedHeroes");
            }
        }


        Hero selectedHero; // выделенный первый элемент
        public Hero SelectedHero
        {
            get
            {
                return selectedHero;
            }
            set
            {
                selectedHero = value;
                onPropertyChanged("SelectedHero");
            }
        }

        Hero newHero; // на данный момент рудимертарный элемент, который возможно ещё пригодиться ( удалить, если нет )
        public Hero NewHero
        {
            get
            {
                return newHero;
            }
            set
            {
                selectedHero = newHero;
                onPropertyChanged("newHero");
            }
        }

        //Реализация паттерна стратегия ( частично это больше Фабричный метод чем стратегия)
        public ApplicationViewModel()
        {
            DataSources = new List<ISource> { new BDmssql(), new JSONRecords() }; //здесь можно добавлять разные источники данных для работы со списком элементов Hero. Каждый источник должен реализовывать интерфейс ISource.
            insertHeroes = new List<Hero>();
            Source = DataSources[0];
            currentWindow = new ViewHeroes();
        }
        public Window currentWindow; // на данный момент рудимент, но может пригодиться в дальнейшем


        //Комманды для взаимодействия с моделью и вьюшкой
        RelayCommand updateCommand;  //
        public RelayCommand UpdateCommand // обработчик кнопки "Обновить". Вносит изменения во все имеющиеся источники. 
        {
            get
            {
                return updateCommand ?? (updateCommand = new RelayCommand(
                    obj =>
                    {
                        foreach(ISource s in DataSources)
                        {
                            s.SaveChanges();
                        }
                    }));
            }
        }

     
        private void CloseCommanddb() // пока не реализованно 
        {
            source.Dispose();
        }

        //реализация инструментария для копипасты + удаление, и вырезания
        #region
        RelayCommand deleteCommand;  
        public RelayCommand DeleteCommand 
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(
                    obj =>
                    {
                        List<Hero> heroesForDeleting = new List<Hero>();
                        foreach (var hero in (IList)obj)
                        {
                            heroesForDeleting.Add(hero as Hero);
                        }
                        foreach(Hero hero in heroesForDeleting)
                        {
                            source.Remove(hero);
                        }
                        onPropertyChanged("HeroesList");
                        
                    }, (obj) => source.Count>0));
            }
        }

        
        RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ?? (copyCommand = new RelayCommand(
                    obj =>
                    {
                        if(insertHeroes.Count>0)
                        {
                            insertHeroes.Clear();
                        }
                        foreach (var hero in (IList)obj)
                        {
                            insertHeroes.Add(hero as Hero);
                        }
                    }, (obj) => selectedHeroes != null && selectedHeroes.Count > 0));
            }
        }

        RelayCommand cutCommand;
        public RelayCommand CutCommand 
        {
            get
            {
                return cutCommand ?? (cutCommand = new RelayCommand(
                    obj =>
                    {
                        if (insertHeroes.Count > 0)
                        {
                            insertHeroes.Clear();
                        }
                        foreach (var hero in (IList)obj)
                        {
                            insertHeroes.Add(hero as Hero);
                        }
                        foreach (Hero hero in insertHeroes)
                        {
                            source.Remove(hero);
                        }
                        onPropertyChanged("HeroesList");

                    }, (obj) => selectedHeroes!=null && selectedHeroes.Count > 0));
            }
        }

        RelayCommand pasteCommand;
        public RelayCommand PasteCommand
        {
            get
            {
                return pasteCommand ?? (pasteCommand = new RelayCommand(
                    obj => 
                    {
                        foreach(Hero hero in insertHeroes)
                        {
                            source.Add(hero);
                        }
                        insertHeroes.Clear();
                    }, (obj) => insertHeroes.Count > 0));
            }
        }
        #endregion


        //добавление нового элемента ( недореализованно до конца: меняет файл при обычном закрытии )
        // TODO: дореализовать данный сегмент с закрытие, + понять, почему меняется порядок изменённого элемента!!! ( Доделать побыстрее!)
        RelayCommand beginChangeCommand;
        public RelayCommand BeginChangeCommand
        {
            get
            {
                return beginChangeCommand ?? (beginChangeCommand = new RelayCommand(
                    obj =>
                    {
                        new ChangeHeroModelViewModel(new ChangeHero(), SelectedHero); //создаётся ViewModel для нового окна по изменеию объекта
                        

                    }, (obj) => selectedHero != null));
            }
        }

        // содержимое ChangeHeroModelViewModel, на данный момент рудимент, однако не буду удалять, пока не доделаю ChangeHeroModelViewModel
        //RelayCommand finishChangeCommand;
        //public RelayCommand FinishChangeCommand
        //{
        //    get
        //    {
        //        return finishChangeCommand ?? (finishChangeCommand = new RelayCommand(
        //            obj =>
        //            {
        //                currentWindow.Close();
        //                //(obj as Hero)
        //            }, obj => {

        //                var results = new List<ValidationResult>();
        //                var context = new ValidationContext(selectedHero);
        //                if (!Validator.TryValidateObject(selectedHero, context, results, true))
        //                {
        //                    return false;
        //                }
        //                return true;
        //            }));
        //    }
        //}


        RelayCommand startAddCommand;
        public RelayCommand StartAddCommand //Добавление нового элемента
        {
            get
            {
                return startAddCommand ?? (startAddCommand = new RelayCommand(
                    obj =>
                    {
                        //новая вьюмодель с вьюшкой в конструкторе
                        source.Add((new AddViewModel(new ViewAdd()).NewHero)); //куонструкция прикольная, но нужно потестить на ошибки ( в старом виде ошибок не было)
                    }));
            }
        }

        //тоже свмое, что и с ChangeHeroModelViewModel
        //RelayCommand finishAddCommand;
        //public RelayCommand FinishAddCommand
        //{
        //    get
        //    {
        //        return finishAddCommand ?? (finishAddCommand = new RelayCommand(
        //            obj =>
        //            {
        //                source.Add(newHero);
        //                currentWindow.Close();
        //            }, obj =>
        //            {
        //                var results = new List<ValidationResult>();
        //                var context = new ValidationContext(newHero);
        //                if (!Validator.TryValidateObject(newHero, context, results, true))
        //                {
        //                    return false;
        //                }
        //                return true;
        //            }));
        //    }
        //}


        public event PropertyChangedEventHandler PropertyChanged; // релация INotifyPropertyChanged
        public void onPropertyChanged(string prop="") //
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
