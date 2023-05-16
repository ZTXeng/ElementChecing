using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Model {
    public class CheckingResultModel : ObservableObject {
        private int _number;
        private string _name;
        private int _id;
        private ObservableCollection<CheckingResultModel> _elements;


        public CheckingResultModel(int number, string name, int id) {
            _number = number;
            _name = name;
            _id = id;
            _elements = new ObservableCollection<CheckingResultModel>();
        }

        public CheckingResultModel() {
            _elements = new ObservableCollection<CheckingResultModel>();
        }

        public ObservableCollection<CheckingResultModel> Elements {
            get => _elements;
            set => SetProperty(ref _elements, value);
        }

        public int Number {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        public int Id {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}
