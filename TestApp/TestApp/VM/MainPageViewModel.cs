using TestApp.Dependency;
using Xamarin.Forms;

namespace TestApp.VM {
    public class MainPageViewModel : BindableObject {

        public MainPageViewModel(IData data) {
            CounterCommand = new Command(CounterAction);
            _counter = data.StartCount;
        }

        private void CounterAction(object obj) {
            Counter+=2;
        }

        private int _counter = 34;
        public int Counter {
            get => _counter;
            set {
                _counter = value;
                OnPropertyChanged();
            }
        }

        public Command CounterCommand { get; set; }
    }
}
