using Autofac;
using ppedv.MessApp.Logic;
using ppedv.MessApp.Model;
using ppedv.MessApp.Model.Contracts;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ppedv.MessApp.UI.WPF.ViewModels
{
    public class MesslaeufeViewModel
    {
        Core _core = null;

        public ObservableCollection<Messlauf> MesslaufListe { get; set; }

        public Messlauf SelectedMesslauf { get; set; }


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

        }
    }
}
