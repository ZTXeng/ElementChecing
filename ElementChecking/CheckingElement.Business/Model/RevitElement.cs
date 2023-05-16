using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Model {
    public class RevitElement : ObservableObject {
        private int _number;
        //private int _id;
        private string _name;
        private int _elementId;
        private bool _isChecked;


        public int Number {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        //public int Id {
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int ElementId {
            get => _elementId;
            set => SetProperty(ref _elementId, value);
        }

        public bool IsChecked {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
    }
}
