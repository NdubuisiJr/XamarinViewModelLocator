using Xamarin.Forms;
using TestApp.Views;
using XamarinViewModelLocator.Config;
using Autofac;
using TestApp.Dependency;
using TestApp.VM;
using XamarinViewModelLocator;

namespace TestApp {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            // Set up container/Register dependencies
            ConfigureAutoFacContainer();

            MainPage = new MainPage();
        }

        private void ConfigureAutoFacContainer() {
            var containerBuilder = new ContainerBuilder();

            // Register dependencies:
            containerBuilder.RegisterType<Data>()
                            .As<IData>().SingleInstance();

            // Register View model
            containerBuilder.RegisterType<MainPageViewModel>();


            // Build Container
            _container = containerBuilder.Build();
            
            // Give View model Locator a refrence to the container
            ViewModelLocator.Config = new Configuration()
                            .SetContainer(_container)
                            .SetFolderNames("Views", "VM");
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }

        private static IContainer _container;
    }
}
