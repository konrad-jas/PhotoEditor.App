using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;

namespace PhotoEditor.ViewModels
{
	public abstract class BaseViewModel : NotificationObject
	{
	    protected readonly IPopupInflater PopupInflater;
	    protected BaseViewModel(IPopupInflater popupInflater)
	    {
	        PopupInflater = popupInflater;
	    }

	    public bool Busy
	    {
	        get { return _busy; }
	        private set
	        {
	            _busy = value;
	            RaisePropertyChanged(() => Busy);
	        }
	    }

	    private readonly AutoResetEvent _resetEvent = new AutoResetEvent(true);
	    private bool _busy;

	    public virtual void Init(object args)
	    {
	    }

		public async void RunInBackground<TArg>(Func<Task<TArg>> backgroundTask, Action<TArg> callbackAction = null)
		{
			await Task.Run(() => _resetEvent.WaitOne());
			Busy = true;
		    try
		    {
		        var result = await Task.Run(async () => await backgroundTask());
		        if (result != null)
		            callbackAction?.Invoke(result);
		    }
		    catch (Exception ex)
		    {
		        var printedException = ex.InnerException ?? ex;
		        Debug.WriteLine(printedException.ToString());
		        await
		            PopupInflater.InflatePopup("Oops", "Something went wrong, please check your connection and try again",
		                "Ok");
		    }

		    Busy = false;
			_resetEvent.Set();
		}
	}
}
