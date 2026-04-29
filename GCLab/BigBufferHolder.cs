namespace GCLab;

static class BigBufferHolder
{
    public static byte[] Run()
    {
        var data = new byte[200_000]; // LOH
        GlobalCache.Add(data);
        return data;
    }

    public static void ClearCache()
    {
        GlobalCache.Clear();
    }
}