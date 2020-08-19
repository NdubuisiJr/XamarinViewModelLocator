using System;
using Xamarin.Forms;

namespace TestApp.ViewModels {
    public class MainPageViewModel : BindableObject {

        public MainPageViewModel(object dependency) {
            CounterCommand = new Command(CounterAction);
            _dependency = dependency;
        }

        private void CounterAction(object obj) {
            Counter++;
        }

        private int _counter;
        public int Counter {
            get => _counter;
            set {
                _counter = value;
                OnPropertyChanged();
            }
        }

        public Command CounterCommand { get; set; }

        private object _dependency;
    }
}
