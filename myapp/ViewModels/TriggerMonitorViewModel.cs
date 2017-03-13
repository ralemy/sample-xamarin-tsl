using System;
using GalaSoft.MvvmLight;
using TechnologySolutions.Rfid.AsciiProtocol;
using Tsl.Core;

namespace myapp
{
public class TriggerMonitorViewModel : ViewModelBase
{
	ReaderService _reader;

	string _triggerAction = "Not Connected";
	public string TriggerAction
	{
		get { return _triggerAction; }
		set
		{
			Set(() => TriggerAction, ref _triggerAction, value);
		}
	}

	public TriggerMonitorViewModel(ReaderService reader)
	{
		_reader = reader;
		_reader.RegisterForConnectionEvents(this, msg =>
		{
			if (msg == null) TriggerAction = "Not Connected";
			else RegisterSwitch(_reader);
		});
	}

	void RegisterSwitch(ReaderService reader)
	{
		TriggerAction = "Not Started";
		reader.SwitchConfig.AsyncReporting = true;
		reader.SwitchConfig.SwitchStateChanged = (sender, e) =>
		{
			switch (e.State)
			{
				case SwitchState.Double:
					TriggerAction = "Double Click";
					break;
				case SwitchState.Single:
					TriggerAction = "Single Click";
					break;
				case SwitchState.Off:
					TriggerAction = "Off";
					break;
			};
		};
		reader.Configure(reader.SwitchConfig);
	}
}
}
