using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoEditor.ViewModels
{
	public abstract class BaseViewModel : NotificationObject
	{
		public bool Busy { get; private set; }
		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public async void RunInBackground<TArg>(Func<Task<TArg>> backgroundTask, Action<TArg> callbackAction = null)
		{
			await Task.Run(() => _resetEvent.WaitOne());
			Busy = true;
			var result = await Task.Run(async () => await backgroundTask());
			if (result != null)
				callbackAction?.Invoke(result);

			Busy = false;
			_resetEvent.Set();
		}
	}
}
