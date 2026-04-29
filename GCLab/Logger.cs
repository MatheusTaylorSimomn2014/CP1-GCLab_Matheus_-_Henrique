using System.Text;

namespace GCLab;

class Logger : IDisposable
{
    private readonly StreamWriter _writer;
    private bool _disposed;

    public Logger(string path)
    {
        _writer = new StreamWriter(path, append: true, Encoding.UTF8);
    }

    public void WriteLines(int count)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(Logger));

        for (int i = 0; i < count; i++)
            _writer.WriteLine($"linha {i}");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _writer.Dispose();
        }

        _disposed = true;
    }

    ~Logger()
    {
        Dispose(false);
    }
}