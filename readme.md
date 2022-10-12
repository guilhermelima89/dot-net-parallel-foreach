# .NET PARALLEL FOREACH

## Back-End

- Net 6
- EF Core
- Swagger

```C#
Parallel.ForEach(pedidos, (PedidoViewModel pedido) =>
{
    //YOUR CODE HERE

    lock (obLock)
    {
       //YOUR CODE HERE
    }
});
```

## EF Core

- dotnet ef migrations add 001
- dotnet ef migrations remove
- dotnet ef database update
- dotnet ef migrations script -o script.sql -i

## Format

- dotnet tool install -g dotnet-format
- dotnet format ./MySolutionFile.sln
- dotnet format ./MyProjectFile.csproj
- dotnet format --verify-no-changes --report issues.json
