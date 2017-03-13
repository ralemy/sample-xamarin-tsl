using System;
using System.Collections.Generic;
using Tsl.Core.ViewModel;
using Xamarin.Forms;

namespace myapp
{
public partial class TriggerMonitorPage : ContentPage
{
	public static readonly string PageKey = "TriggerMonitorPage";
	public TriggerMonitorPage()
	{
		InitializeComponent();
		BindingContext = ViewModelLocator.GetDependency<TriggerMonitorViewModel>();
	}
}
}
