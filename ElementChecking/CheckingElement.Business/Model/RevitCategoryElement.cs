using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Model {
    public class RevitCategoryElement : ObservableObject {
        private string _name;
        private bool _isChecked;
        private int _id;
        private ObservableCollection<RevitElement> _elements;

        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int Id {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public bool IsChecked {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public ObservableCollection<RevitElement> Elements {
            get => _elements;
            set => SetProperty(ref _elements, value);
        }

        public RevitCategoryElement() {
            _elements = new ObservableCollection<RevitElement>();
        }
    }
}
