using System;

namespace Padutronics.Disposing;

public interface INotifyDisposed
{
    event EventHandler<DisposedEventArgs>? Disposed;
}