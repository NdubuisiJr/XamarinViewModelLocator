using System;

namespace XamarinViewModelLocator.Config {
    /// <summary>
    /// A class that changes the default behavior of the view model locator
    /// </summary>
    public class Configuration {

        /// <summary>
        /// Sets the container instance used for resolving view models and 
        /// their dependencies
        /// </summary>
        /// <typeparam name="T">The type of the container object</typeparam>
        /// <param name="containerObject">An instance of the container where all the project dependencies has been registered</param>
        /// <param name="resolveMethodName">The name of the method in the container used for resolving dependencies</param>
        /// <returns></returns>
        public Configuration SetContainer<T>(T containerObject, string resolveMethodName="Resolve")
            where T: class {
            Container = containerObject;
            ContainerType = typeof(T);
            ResolveMethodName = resolveMethodName;
            return this;
        }

        /// <summary>
        /// Changes the default folder names or namespaces where view model locator will look for the views and
        /// their corresponding view models
        /// </summary>
        /// <param name="viewFolderName">Folder name where views are located. The default is "Views"</param>
        /// <param name="viewModelFolderName">Folder name where view models are located. The default is "ViewModels"</param>
        /// <returns></returns>
        public Configuration SetFolderNames(string viewFolderName, string viewModelFolderName) {
            ViewFolderName = viewFolderName;
            ViewModelFolderName = viewModelFolderName;
            return this;
        }

        public Type ContainerType { get; private set; }
        public object Container { get; private set; }
        public string ResolveMethodName { get; private set; } = "Resolve";
        public string ViewFolderName { get; private set; } = "Views";
        public string ViewModelFolderName { get; private set; } = "ViewModels";
    }
}
