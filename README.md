This repo is used only to demonstrate the behavior with following error [here](https://github.com/dotnet/efcore/issues/26897)
```
System.ArgumentException: GenericArguments[1], 'System.Collections.Generic.IReadOnlyCollection`1[SomeObject]', on 'TCollection InitializeCollection[TElement,TCollection](Int32, Microsoft.EntityFrameworkCore.Query.QueryContext, System.Data.Common.DbDataReader, Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryResultCoordinator, System.Func`3[Microsoft.EntityFrameworkCore.Query.QueryContext,System.Data.Common.DbDataReader,System.Object[]], System.Func`3[Microsoft.EntityFrameworkCore.Query.QueryContext,System.Data.Common.DbDataReader,System.Object[]], Microsoft.EntityFrameworkCore.Metadata.IClrCollectionAccessor)' violates the constraint of type 'T
```

## How to run?

1. Make sure you have .net 6 SDK installed
2. Run
```
dotnet test
```
