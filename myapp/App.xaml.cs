using System;
using Tsl.Core;
using Tsl.Core.ViewModel;
using Xamarin.Forms;

namespace myapp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			RegisterDependencies();
			MainPage = RegisterPages(new NavigationPage(new MainPage()));
		}

		Page RegisterPages(NavigationPage navigationPage)
		{
			var navigation = ViewModelLocator.GetDependency<INavigationManager>();

			navigation.SetMain(navigationPage);
			ViewModelLocator.RegisterPages(navigation);

			navigation.Register(TriggerMonitorPage.PageKey, typeof(TriggerMonitorPage));
			navigation.Register(InventoryPage.PageKey, typeof(InventoryPage));

			return navigationPage;
		}

		void RegisterDependencies()
		{
			ViewModelLocator.InjectDependencies();
			ViewModelLocator.Register<MainViewModel>();
			ViewModelLocator.Register<TriggerMonitorViewModel>();
			ViewModelLocator.Register<InventoryModelView>();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
