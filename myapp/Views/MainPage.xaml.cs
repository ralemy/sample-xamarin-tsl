using System;
using System.Collections.Generic;
using Tsl.Core.ViewModel;
using Xamarin.Forms;

namespace myapp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = ViewModelLocator.GetDependency<MainViewModel>();
		}
	}
}
