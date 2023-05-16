using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.ViewModel {
    public abstract class ViewModelBase<TModel> : ObservableRecipient {
        public TModel Model { get; set; }
    }
}
