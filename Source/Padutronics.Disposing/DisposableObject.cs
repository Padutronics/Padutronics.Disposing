using System;
using System.Diagnostics;
using System.Text;

namespace Padutronics.Disposing;

public abstract class DisposableObject : IDisposable
{
#if DEBUG
    private readonly StackFrame creatingStackTrace = new(skipFrames: 1, needFileInfo: false);
#endif

    private bool isDisposed;

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

#if DEBUG
    private string ObjectName => GetType().FullName ?? throw new Exception("Type name is null.");
#endif

    public void Dispose()
    {
        TryDispose(isDisposing: true);
    }

    protected abstract void Dispose(bool isDisposing);

    private void MarkAsDisposed()
    {
        GC.SuppressFinalize(this);

        isDisposed = true;
    }

    private void TryDispose(bool isDisposing)
    {
        if (!isDisposed)
        {
            Dispose(isDisposing);
            MarkAsDisposed();
        }
    }
}