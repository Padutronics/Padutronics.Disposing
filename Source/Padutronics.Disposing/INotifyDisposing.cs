using System;

namespace Padutronics.Disposing;

public interface INotifyDisposing
{
    event EventHandler<DisposingEventArgs>? Disposing;
}