using System;
using System.Globalization;
using System.Linq;
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
            try {
                var viewAndModelType = GetViewAndViewModelType(bindable);
                if (viewAndModelType.Item1 == null)
                    return;

                object viewModel = null;
                var view = viewAndModelType.Item1;
                var viewModelType = viewAndModelType.Item2;
                if (viewModelType.GetConstructor(Type.EmptyTypes) != null) {
                    viewModel = Activator.CreateInstance(viewModelType);
                }
                else if (Config.Container != null) {
                    var resolveMethods = from type in Config.ContainerType.Assembly.GetTypes()
                                         where !type.IsGenericType && !type.IsNested
                                         from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                         select method;

                    var resolveMethod = resolveMethods.FirstOrDefault(x => x.Name == Config.ResolveMethodName);
                    if (resolveMethod.IsGenericMethod)
                        viewModel = resolveMethod.MakeGenericMethod(viewModelType).Invoke(Config.Container, new object[] { Config.Container });
                    else
                        viewModel = resolveMethod.Invoke(Config.Container, new object[] { viewModelType });
                }
                view.BindingContext = viewModel;
            }
            catch {

                throw new LocatorException("Unable to locate view model. This maybe due to either of these:\n1.The view model does not have" +
                    "a parameterless constructor.\n2.The dependencies of the view model and the view model itself are not registered in the given container");
            }
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

    public class LocatorException : Exception {
        public LocatorException(string message): base(message) {
        }
    }
}
