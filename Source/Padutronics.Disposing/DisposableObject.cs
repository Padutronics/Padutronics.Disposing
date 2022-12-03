using System;
using System.Diagnostics;
using System.Text;

namespace Padutronics.Disposing;

public abstract class DisposableObject : IDisposableObject
{
#if DEBUG
    private readonly StackFrame creatingStackTrace = new(skipFrames: 1, needFileInfo: false);
#endif

    ~DisposableObject()
    {
#if DEBUG
        var message = new StringBuilder();
        message.AppendLine($"Object is being finalized: {ObjectName}.");
        message.AppendLine("Object creation stack trace:");
        message.Append(creatingStackTrace);

        Debug.Assert(condition: false, message.ToString());
#endif

        TryDispose(isDisposing: false);
    }

    public bool IsDisposed { get; private set; }

#if DEBUG
    private string ObjectName => GetType().FullName ?? throw new Exception("Type name is null.");
#endif

    public event EventHandler<DisposedEventArgs>? Disposed;
    public event EventHandler<DisposingEventArgs>? Disposing;

    public void Dispose()
    {
        TryDispose(isDisposing: true);
    }

    protected abstract void Dispose(bool isDisposing);

    private void MarkAsDisposed()
    {
        GC.SuppressFinalize(this);

        IsDisposed = true;
    }

    protected virtual void OnDisposed(DisposedEventArgs e)
    {
        Disposed?.Invoke(this, e);
    }

    protected virtual void OnDisposing(DisposingEventArgs e)
    {
        Disposing?.Invoke(this, e);
    }

    private void TryDispose(bool isDisposing)
    {
        if (!IsDisposed)
        {
            OnDisposing(DisposingEventArgs.Empty);

            Dispose(isDisposing);
            MarkAsDisposed();

            OnDisposed(DisposedEventArgs.Empty);
        }
    }
}