using System;

namespace Padutronics.Disposing;

public interface IDisposableObject : IDisposable, INotifyDisposed, INotifyDisposing
{
    bool IsDisposed { get; }
}