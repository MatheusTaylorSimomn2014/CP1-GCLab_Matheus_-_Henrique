# GCLab - Laboratório de Garbage Collection em .NET

----

## Nomes e RM dos Integrantes
- Matheus Taylor, RM556211
- Henrique Maldonado

---

## Visão Geral

GCLab é um projeto educacional desenvolvido em C# (.NET 8) que demonstra os principais desafios e boas práticas relacionadas ao Garbage Collection (GC) no .NET. O laboratório simula cenários comuns de vazamento de memória e problemas de gerenciamento de recursos, fornecendo uma versão corrigida que implementa as soluções adequadas.

## Objetivo do Projeto

O principal objetivo é demonstrar como evitar que objetos permaneçam vivos na memória após não serem mais necessários, prevenindo vazamentos de memória e garantindo que o Garbage Collector possa liberar recursos adequadamente.

## Cenários Demonstrados

### 1. **Vazamento por Eventos (LeakySubscriber)**
**Problema**: Inscrições em eventos que não são removidas mantêm referências vivas.
**Solução**: Implementação correta de `IDisposable` com remoção explícita de handlers de eventos.

### 2. **Large Object Heap (LOH)**
**Problema**: Objetos grandes (>85KB) são alocados diretamente na LOH e podem causar fragmentação.
**Solução**: Limpeza explícita de caches globais e referências a objetos grandes.

### 3. **Pinned Objects (Objetos Fixados)**
**Problema**: Objetos fixados via `GCHandle` podem impedir a compactação do heap.
**Solução**: Liberação adequada de handles usando `IDisposable` e padrão de finalizador.

### 4. **Caches Globais**
**Problema**: Coleções estáticas que mantêm referências indefinidamente.
**Solução**: Implementação de métodos de limpeza explícitos.

## Estrutura do Projeto

### Classes Principais

- **`Program`**: Ponto de entrada que orquestra o experimento e verifica sobreviventes.
- **`IssueTracker`**: Monitora objetos via `WeakReference` para detectar vazamentos.
- **`GCHelpers`**: Utilitários para forçar coleta de lixo completa.

### Cenários de Teste

- **`LeakySubscriber`**: Demonstra gerenciamento correto de eventos.
- **`BigBufferHolder`**: Manipulação de objetos na LOH.
- **`Pinner`**: Gerenciamento de memória pinada.
- **`ConcatWork`**: Otimização de concatenação de strings.
- **`Logger`**: Gerenciamento de recursos não-gerenciados.
- **`GlobalCache`**: Cache estático com limpeza controlada.

## Padrões Implementados

### Dispose Pattern
Todas as classes que gerenciam recursos implementam `IDisposable` corretamente:
- `Dispose()` público
- `Dispose(bool)` protegido para herança
- Finalizador como fallback
- `GC.SuppressFinalize()` para evitar finalização dupla

### Weak References
Uso de `WeakReference` para monitorar objetos sem impedir sua coleta.

### Limpeza Explícita
- Remoção de handlers de eventos
- Limpeza de caches estáticos
- Liberação de handles nativos

## Como Executar

1. Certifique-se de ter o .NET 8 SDK instalado
2. Clone o repositório
3. Execute o projeto:
```bash
dotnet run
```

## Resultados Esperados

A execução bem-sucedida deve mostrar:
- Zero sobreviventes após a coleta forçada
- Mensagem: "✅ GC limpo: nenhuma referência indesejada permaneceu viva."
- Todas as referências rastreadas como "coletado"

## Lições Aprendidas

1. **Sempre desinscreva-se de eventos**: Handlers de eventos são uma das causas mais comuns de vazamentos.
2. **Implemente IDisposable corretamente**: Use o padrão completo para recursos não-gerenciados.
3. **Cuidado com caches estáticos**: Sempre forneça mecanismos de limpeza.
4. **Libere objetos pinados**: `GCHandle` deve ser explicitamente liberado.
5. **Prefira StringBuilder**: Para concatenações pesadas de strings.
6. **Use WeakReference**: Quando precisar rastrear objetos sem manter referências fortes.

## Modo de Coleta de Lixo

O projeto pode funcionar tanto em Workstation GC quanto em Server GC. O modo padrão é exibido no início da execução e pode afetar o comportamento da coleta de lixo.

## Referências

- [Documentação oficial do GC no .NET](https://docs.microsoft.com/dotnet/standard/garbage-collection/)
- [IDisposable Pattern](https://docs.microsoft.com/dotnet/standard/garbage-collection/implementing-dispose)
- [Large Object Heap](https://docs.microsoft.com/dotnet/standard/garbage-collection/large-object-heap)

