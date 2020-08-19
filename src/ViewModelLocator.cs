using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using XamarinViewModelLocator.Config;

namespace XamarinViewModelLocator {
    public static class ViewModelLocator {

        static ViewModelLocator() {
            Config = new Configuration();
        }

        public static readonly BindableProperty AutoWireViewModelProperty =
           BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool),
               typeof(ViewModelLocator), default(bool),
               propertyChanged: OnAutoWireViewModelChanged);

        private static void OnAutoWireViewModelChanged(BindableObject bindable,
                    object oldValue, object newValue) {
            var viewAndModelType = GetViewAndViewModelType(bindable);
            if (viewAndModelType.Item1 == null )
                return;

            object viewModel;
            var view = viewAndModelType.Item1;
            var viewModelType = viewAndModelType.Item2;
            if (Config.Container is null) {
                viewModel = Activator.CreateInstance(viewModelType);
            }
            else {
                var resolveMethod = Config.ContainerType.GetMethod(Config.ResolveMethodName);
                viewModel = resolveMethod.Invoke(Config.Container, new object[] { viewAndModelType });
            }
            view.BindingContext = viewModel;
        }

        public static bool GetAutoWireViewModel(BindableObject bindable) {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value) {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        private static (Element, Type) GetViewAndViewModelType(BindableObject bindable) {
            var view = bindable as Element;
            if (view == null) {
                return (null, null);
            }
            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace($".{Config.ViewFolderName}.", $".{Config.ViewModelFolderName}.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}",
                viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null) {
                return (null, null);
            }
            return (view, viewModelType);
        }

        public static Configuration Config { get; set; }
    }
}
