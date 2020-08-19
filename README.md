 <div align="center" >
 
 [![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)
 [![forthebadge](https://forthebadge.com/images/badges/makes-people-smile.svg)](https://forthebadge.com)
 
 [![GitHub license](https://img.shields.io/github/license/NdubuisiJr/XamarinViewModelLocator.svg?style=flat-square)](https://github.com/NdubuisiJr/XamarinViewModelLocator/blob/master/LICENSE)
 [![Actions Status](https://github.com/NdubuisiJr/XamarinViewModelLocator/workflows/Build/badge.svg?style=flat-square)](https://github.com/NdubuisiJr/XamarinViewModelLocator/actions)
  [![Actions Status](https://github.com/NdubuisiJr/XamarinViewModelLocator/workflows/Deployment/badge.svg?style=flat-square)](https://github.com/NdubuisiJr/XamarinViewModelLocator/actions)
 [![GitHub last commit](https://img.shields.io/github/last-commit/NdubuisiJr/XamarinViewModelLocator.svg?style=flat-square)](https://github.com/NdubuisiJr/XamarinViewModelLocator)
  [![nuget](https://img.shields.io/nuget/v/Xam.ViewModelLocator.svg?style=flat-square)](https://www.nuget.org/packages/Xam.ViewModelLocator/)
</div><br>

# XamarinViewModelLocator
A super light-weight view model locator for Xamarin.Forms applications

# Getting Started

## Installation
* Install nuget package(Xam.ViewModelLocator) into your .Net Standard or PCL project
* Import name space in the view file and set the AutoWireViewModelProperty to true
```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             xmlns:viewModelLocator="clr-namespace:XamarinViewModelLocator;assembly=XamarinViewModelLocator"
             viewModelLocator:ViewModelLocator.AutoWireViewModel="True"
             x:Class="TestApp.Views.MainPage">
 </ContentPage>
```
That's all...

## Naming Convention
* View Name can be anything
* View Model's name must be ViewName+"ViewModel". e.g View's Name = MainPage, ViewModel's Name = MainPageViewModel
* Soon this convention can be changed

## Folder Convention
By convention it will look for the view in a "Views" folder/namespace and the view model in a "ViewModels" folder/namespace.
This convention can be changed by passing a config file to the viewModel object on start up.
```
ViewModelLocator.Config = new Configuration().SetFolderNames("NewViewsFolderName", "NewViewModelsFolderName");
```
## Construction Convention
* If the view model has a parameterless constructor. It is used to create the object.
* If the view model has dependencies, then the view model and it's dependencies will be resolved from a given container refrence.
* Container refrence can be supplied by chaining a new method to the `configuration` object.
```
ViewModelLocator.Config = new Configuration().SetFolderNames("NewViewsFolderName", "NewViewModelsFolderName")
                                             .SetContainer(_containerRefrenceObject);
```
Note: Only chain the methods you need.

## Supported Containers
* AutoFac
* Unity

## Contributing
* Create a Fork from this repository.
* Clone your fork into your work station.
* Switch to the development branch.
* Make your changes on the development branch.
* Push your changes to your fork.
* Create a pull request back to the main repository.
* Add a new remote called upstream to point to the main repository.
