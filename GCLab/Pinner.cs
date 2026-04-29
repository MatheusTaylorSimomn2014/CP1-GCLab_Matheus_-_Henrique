using System.Runtime.InteropServices;

namespace GCLab;

class Pinner : IDisposable
{
    private GCHandle _handle;
    private bool _disposed;

    public byte[] PinLongTime()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(Pinner));

        var data = new byte[256];

        _handle = GCHandle.Alloc(data, GCHandleType.Pinned);

        return data;
    }

    public void Dispose()
    {
        if (_disposed) return;

        if (_handle.IsAllocated)
            _handle.Free();

        _disposed = true;

        GC.SuppressFinalize(this);
    }

    ~Pinner()
    {
        Dispose();
    }
}
