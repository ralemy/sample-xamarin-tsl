using System;
using System.Collections.Generic;
using Tsl.Core.ViewModel;
using Xamarin.Forms;

namespace myapp
{
	public partial class InventoryPage : ContentPage
	{
		public static readonly string PageKey = "InventoryPage";
public InventoryPage()
{
	var powerStep = 0.5;
	InitializeComponent();
	PowerSlider.ValueChanged += (sender, e) => {
		var newStep = Math.Round(e.NewValue / powerStep);
		PowerSlider.Value = newStep * powerStep;
	};
	BindingContext = ViewModelLocator.GetDependency<InventoryViewModel>();
}
	}
}
