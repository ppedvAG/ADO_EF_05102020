using Autofac;
using ppedv.MessApp.Logic;
using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ppedv.MessApp.UI.WPF.ViewModels
{
    public class MesslaeufeViewModel
    {
        Core _core = null;

        public ObservableCollection<Messlauf> MesslaufListe { get; set; }

        public Messlauf SelectedMesslauf { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public MesslaeufeViewModel() : this(null)
        { }

        public MesslaeufeViewModel(Core core)
        {
            //core = new Core(null); //sollte so automatisch klappen...
            if (core == null)
                _core = new Core(((App)Application.Current).Container.Resolve<IUnitOfWork>());
            else
                _core = core;

            MesslaufListe = new ObservableCollection<Messlauf>(_core.UnitOfWork.MesslaufRepository.Query().ToList());
            SaveCommand = new RelayCommand(x => _core.UnitOfWork.SaveAll());
            DeleteCommand = new RelayCommand(x => 
            {
                if (SelectedMesslauf == null)
                    return;
                _core.UnitOfWork.GetRepo<Messlauf>().Delete(SelectedMesslauf);
                MesslaufListe.Remove(SelectedMesslauf);
            });
            NewCommand = new RelayCommand(CreateNewMesslauf);
        }

        private void CreateNewMesslauf(object obj)
        {
            var ml = new Messlauf() { GemessenesGerät = "NEU", GestartetVon = "Peter", Start = DateTime.Now };
            _core.UnitOfWork.MesslaufRepository.Add(ml);
            MesslaufListe.Add(ml);
            SelectedMesslauf = ml;
        }
    }
}
