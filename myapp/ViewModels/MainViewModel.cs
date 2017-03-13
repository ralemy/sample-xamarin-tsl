using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Tsl.Core;
using Tsl.Core.ViewModel;

namespace myapp
{
	public class MainViewModel : ViewModelBase, IDisposable
	{
		private readonly INavigationService _navigator;
		public ICommand GoToConnectPage { get; private set; }
		public ICommand GoToTriggerMonitorPage { get; private set; }
		public ICommand GoToInventoryPage { get; private set; }

		private readonly ReaderService _readerService;
		private string _readerName = "Not Connected";
		public string ReaderName
		{
			get { return _readerName; }
			private set
			{
				Set(() => ReaderName, ref _readerName, value);
			}
		}


		public MainViewModel(INavigationService navigator, ReaderService readerService)
		{
			_navigator = navigator;
			_readerService = readerService;

			GoToConnectPage = new RelayCommand(NavigateToConnectPage);
			GoToTriggerMonitorPage = new RelayCommand(NavigateToTriggerMonitorPage);
			GoToInventoryPage = new RelayCommand(NavigateToInventoryPage);
			_readerService.RegisterForConnectionEvents(this, msg =>
			{
				ReaderName = msg.Content == null ? null : _readerService.ConnectedReader.DisplayName;
			});
		}

		void NavigateToInventoryPage()
		{
			_navigator.NavigateTo(InventoryPage.PageKey);
		}

		void NavigateToTriggerMonitorPage()
		{
			_navigator.NavigateTo(TriggerMonitorPage.PageKey);
		}

		void NavigateToConnectPage()
		{
			_navigator.NavigateTo(ViewModelLocator.ConnectPageKey);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
