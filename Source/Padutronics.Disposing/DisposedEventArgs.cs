using System;

namespace Padutronics.Disposing;

public sealed class DisposedEventArgs : EventArgs
{
    public new static DisposedEventArgs Empty { get; } = new DisposedEventArgs();
}