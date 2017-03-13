using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Tsl.Core;

namespace myapp
{
	public class InventoryModelView : ViewModelBase
	{
		private readonly ReaderService _readerService;
		public ICommand UpdateConfig { get; private set; }
public ICommand ScanBarcode { get; private set; }
public ICommand RunInventory { get; private set; }

		private bool _shouldUpdate = true;
		public bool ShouldUpdate
		{
			get { return _shouldUpdate; }
			set
			{
				_shouldUpdate = value;
				(UpdateConfig as RelayCommand).RaiseCanExecuteChanged();
				(ScanBarcode as RelayCommand).RaiseCanExecuteChanged();
				(RunInventory as RelayCommand).RaiseCanExecuteChanged();
			}
		}

		private double _outputPower = 29.0;
		public double OutputPower
		{
			get { return _outputPower; }
			set
			{
				if (Set(() => OutputPower, ref _outputPower, value))
					ShouldUpdate = true;
			}
		}

		public int MaxOutputPower
		{
			get { return _readerService.InventoryConfig.MaxOutputPower; }
		}
		public int MinOutputPower
		{
			get { return _readerService.InventoryConfig.MinOutputPower; }
		}

		private string _lastEpc = "";
		public string LastEpc
		{
			get { return _lastEpc; }
			set { Set(() => LastEpc, ref _lastEpc, value); }
		}

		private int _scanCount = 0;
		public int ScanCount
		{
			get { return _scanCount; }
			set { Set(() => ScanCount, ref _scanCount, value); }
		}

		private string _lastBarcode = "";
		public string LastBarcode
		{
			get { return _lastBarcode; }
			set { Set(() => LastBarcode, ref _lastBarcode, value); }
		}


		public InventoryModelView(ReaderService readerService)
		{
			_readerService = readerService;
Func<bool> hasReader = () => _readerService.ConnectedReader != null;

			UpdateConfig = new RelayCommand(ConfigReader, () => hasReader() && ShouldUpdate);
			ScanBarcode = new RelayCommand(() => _readerService.GetBarcode(_readerService.BarcodeConfig),
										   hasReader);
			RunInventory = new RelayCommand(() => _readerService.GetInventory(_readerService.InventoryConfig),
											hasReader);

			_readerService.RegisterForConnectionEvents(this, msg =>
			{
				ShouldUpdate = true;
			});

			_readerService.RegisterTagListener((sender, e) =>
			{
				var i = e.Tags.Count();
				if (i > 0)
					LastEpc = e.Tags.ElementAt(i - 1).Epc;
				if (e.Complete)
					ScanCount += 1;
			});
			_readerService.RegisterBarcodeListener((sender, e) =>
			{
				if (!String.IsNullOrWhiteSpace(e.Barcode))
					LastBarcode = e.Barcode;
			});
		}

		async void ConfigReader()
		{
			_readerService.InventoryConfig.Power = Convert.ToInt32(OutputPower);
			_readerService.InventoryConfig.IncludeRssi = true;
			await _readerService.Configure(_readerService.InventoryConfig);
			ShouldUpdate = false;
		}
	}
}
