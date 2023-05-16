using CheckingElement.Business.Model;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.ViewModel {
    public class CheckingResultViewModel : ViewModelBase<CheckingResultModel> {

        public IAsyncRelayCommand Loaded { get; set; }
        IList<RevitElement> _elements;
        public CheckingResultViewModel(IList<RevitElement> elems) {
            _elements = elems;
            Model = new CheckingResultModel();
            Loaded = new AsyncRelayCommand(OnLoaded);
        }

        private Task OnLoaded() {
            for (int i = 0; i < _elements.Count; i++) {
                var elem = _elements[i];
                var model = new CheckingResultModel(++i, elem.Name, elem.ElementId);
                Model.Elements.Add(model);
            }
            return Task.CompletedTask;
        }
    }
}
