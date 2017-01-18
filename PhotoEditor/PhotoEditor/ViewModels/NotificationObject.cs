using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PhotoEditor.ViewModels
{
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpr = propertyExpression.Body as MemberExpression;
            var memberName = memberExpr?.Member.Name;
            if (memberName != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
