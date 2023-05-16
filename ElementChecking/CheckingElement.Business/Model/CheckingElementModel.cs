using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Model {
    public class CheckingElementModel : ObservableObject {
        private Profession _selectedProfession= Profession.NONE;
        private ObservableCollection<Profession> _professions;
        private ObservableCollection<RevitCategoryElement> _elements;

        public Profession SelectedProfession {
            get => _selectedProfession;
            set => SetProperty(ref _selectedProfession, value);
        }

        public ObservableCollection<Profession> Professions {
            get => _professions;
            set => SetProperty(ref _professions, value);
        }

        public ObservableCollection<RevitCategoryElement> Elements {
            get => _elements;
            set => SetProperty(ref _elements, value);
        }

        public CheckingElementModel() {
            _professions = new ObservableCollection<Profession>();
            _elements = new ObservableCollection<RevitCategoryElement>();
        }
    }
}
