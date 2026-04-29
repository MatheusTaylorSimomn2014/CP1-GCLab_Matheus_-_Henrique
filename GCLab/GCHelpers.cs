namespace GCLab;

static class GCHelpers
{
    public static void FullCollect()
    {
        GC.Collect(
            GC.MaxGeneration,
            GCCollectionMode.Forced,
            blocking: true,
            compacting: true);

        GC.WaitForPendingFinalizers();

        GC.Collect(
            GC.MaxGeneration,
            GCCollectionMode.Forced,
            blocking: true,
            compacting: true);
    }
}