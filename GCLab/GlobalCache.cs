namespace GCLab;

static class GlobalCache
{
    private static readonly List<byte[]> _cache = new();

    public static void Add(byte[] data)
    {
        _cache.Add(data);
    }

    public static void Clear()
    {
        _cache.Clear();
    }
}