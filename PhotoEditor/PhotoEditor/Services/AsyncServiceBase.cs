using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PhotoEditor.Services
{
	public abstract class AsyncServiceBase
	{
		protected static TaskCompletionSource<T> CreateCompletionSource<T>()
		{
			return new TaskCompletionSource<T>(TaskCreationOptions.None);
		}

		protected static void CompleteTask<TResult>(TaskCompletionSource<TResult> completionSource,
			AsyncCompletedEventArgs args, Func<TResult> unwrapResult)
		{
			if (args.Cancelled)
				completionSource.TrySetCanceled();
			else if (args.Error != null)
				completionSource.TrySetException(args.Error);
			else completionSource.TrySetResult(unwrapResult());
		}

		protected static Task<TResult> CreateContinuationTask<TResult>(Task<TResult> baseTask, Action continuationAction)
		{
			return baseTask.ContinueWith(task =>
			{
				continuationAction();
				return task.Result;
			});
		}
	}
}
