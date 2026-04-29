namespace GCLab;

class Program
{
    // VERSÃO CORRIGIDA:
    // Ajustes:
    // - Corrigido nome do método ExecuteWorks
    // - Uso correto de using para objetos descartáveis
    // - Garantia de limpeza de eventos
    // - Limpeza explícita de caches
    // - Liberação adequada de buffers pinned
    // - Evita Dispose duplicado
    static void Main()
    {
        var tracker = new IssueTracker();

        ExecuteWorks(tracker);

        // Força coleta completa
        GCHelpers.FullCollect();
        tracker.Report();

        Console.WriteLine(tracker.HasSurvivors
            ? "\n❌ Existem sobreviventes indesejados. Sua missão: corrigir o código e rodar novamente."
            : "\n✅ GC limpo: nenhuma referência indesejada permaneceu viva.");
    }

    static void ExecuteWorks(IssueTracker tracker)
    {
        Console.WriteLine("=== GCLab - Versão Corrigida ===");
        Console.WriteLine($"GC Server Mode: {System.Runtime.GCSettings.IsServerGC}\n");

        // 1) Evento corrigido com Dispose adequado
        var publisher = new Publisher();

        using (var subscriber = new LeakySubscriber(publisher))
        {
            tracker.Track("subscriber", subscriber);

            // 2) LOH + cache controlado
            var lohBuffer = BigBufferHolder.Run();
            tracker.Track("lohBuffer", lohBuffer);

            // 3) Pinned buffer com descarte correto
            using (var pinner = new Pinner())
            {
                var pinned = pinner.PinLongTime();
                tracker.Track("pinnedBuffer", pinned);

                // 4) String otimizada
                var payload = ConcatWork.Good();
                Console.WriteLine($"Payload length: {payload.Length}");

                // 5) Recurso externo corretamente descartado
                using (var logger = new Logger("log.txt"))
                {
                    logger.WriteLines(10);
                    tracker.Track("logger", logger);

                    // Dispara evento
                    publisher.Raise();
                }

                // Solta referência pinned
                pinned = null;
            }

            // Limpa subscriber registry
            LeakySubscriber.ClearRegistry();
        }

        // Limpeza global
        publisher = null;
        BigBufferHolder.ClearCache();
        GlobalCache.Clear();
    }
}