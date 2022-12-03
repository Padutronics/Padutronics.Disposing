using System;

namespace Padutronics.Disposing;

public sealed class DisposingEventArgs : EventArgs
{
    public new static DisposingEventArgs Empty { get; } = new DisposingEventArgs();
}