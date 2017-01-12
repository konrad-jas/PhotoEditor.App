using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoEditor.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var memberExpr = propertyExpression.Body as MemberExpression;
			var memberName = memberExpr?.Member.Name;
			if(memberName != null)
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}
