Minimal project to show problem described at https://github.com/aspnet/AspNetCore/issues/18076

The issue is using proxies in efcore with blazor. If an object is passed to a sub-component that is accessing
entity property sometime after the page has been loaded, context may have already been disposed by then.

Lazy Loading Proxies are needed to avoid cartesian explosion in master/detail style user interfaces,
 where each entity in main list has its own sublist of owned entities, that in turn, have their own joins.

After some clicking around `fetchdata` page, crashes with

```
Unhandled exception rendering component: Error generated for warning 'Microsoft.EntityFrameworkCore.Infrastructure.LazyLoadOnDisposedContextWarning': An attempt was made to lazy-load navigation property 'Description' on entity type 'WeatherForecastProxy' after the ass
ociated DbContext was disposed. This exception can be suppressed or logged by passing event ID 'CoreEventId.LazyLoadOnDisposedContextWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.
System.InvalidOperationException: Error generated for warning 'Microsoft.EntityFrameworkCore.Infrastructure.LazyLoadOnDisposedContextWarning': An attempt was made to lazy-load navigation property 'Description' on entity type 'WeatherForecastProxy' after the associated DbCon
text was disposed. This exception can be suppressed or logged by passing event ID 'CoreEventId.LazyLoadOnDisposedContextWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.
   at Microsoft.EntityFrameworkCore.Diagnostics.EventDefinition`2.Log[TLoggerCategory](IDiagnosticsLogger`1 logger, WarningBehavior warningBehavior, TParam1 arg1, TParam2 arg2)
   at Microsoft.EntityFrameworkCore.Diagnostics.CoreLoggerExtensions.LazyLoadOnDisposedContextWarning(IDiagnosticsLogger`1 diagnostics, DbContext context, Object entityType, String navigationName)
   at Microsoft.EntityFrameworkCore.Internal.LazyLoader.ShouldLoad(Object entity, String navigationName, NavigationEntry& navigationEntry)
   at Microsoft.EntityFrameworkCore.Internal.LazyLoader.Load(Object entity, String navigationName)
   at Microsoft.EntityFrameworkCore.Proxies.Internal.LazyLoadingInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.Proxies.WeatherForecastProxy.get_Description()
   at sample_use_proxy.Pages.WeatherDetails.BuildRenderTree(RenderTreeBuilder __builder)
   at Microsoft.AspNetCore.Components.ComponentBase.<.ctor>b__6_0(RenderTreeBuilder builder)
   at Microsoft.AspNetCore.Components.Rendering.ComponentState.RenderIntoBatch(RenderBatchBuilder batchBuilder, RenderFragment renderFragment)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.RenderInExistingBatch(RenderQueueEntry renderQueueEntry)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.ProcessRenderQueue()
fail: Microsoft.AspNetCore.Components.Server.Circuits.CircuitHost[111]
      Unhandled exception in circuit 'iorggCZTbXKkp2XYKgVSJ7gAX9Zh3gJzhaCLmA6fiJE'.
System.InvalidOperationException: Error generated for warning 'Microsoft.EntityFrameworkCore.Infrastructure.LazyLoadOnDisposedContextWarning': An attempt was made to lazy-load navigation property 'Description' on entity type 'WeatherForecastProxy' after the associated DbCon
text was disposed. This exception can be suppressed or logged by passing event ID 'CoreEventId.LazyLoadOnDisposedContextWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.
   at Microsoft.EntityFrameworkCore.Diagnostics.EventDefinition`2.Log[TLoggerCategory](IDiagnosticsLogger`1 logger, WarningBehavior warningBehavior, TParam1 arg1, TParam2 arg2)
   at Microsoft.EntityFrameworkCore.Diagnostics.CoreLoggerExtensions.LazyLoadOnDisposedContextWarning(IDiagnosticsLogger`1 diagnostics, DbContext context, Object entityType, String navigationName)
   at Microsoft.EntityFrameworkCore.Internal.LazyLoader.ShouldLoad(Object entity, String navigationName, NavigationEntry& navigationEntry)
   at Microsoft.EntityFrameworkCore.Internal.LazyLoader.Load(Object entity, String navigationName)
   at Microsoft.EntityFrameworkCore.Proxies.Internal.LazyLoadingInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.Proxies.WeatherForecastProxy.get_Description()
   at sample_use_proxy.Pages.WeatherDetails.BuildRenderTree(RenderTreeBuilder __builder)
   at Microsoft.AspNetCore.Components.ComponentBase.<.ctor>b__6_0(RenderTreeBuilder builder)
   at Microsoft.AspNetCore.Components.Rendering.ComponentState.RenderIntoBatch(RenderBatchBuilder batchBuilder, RenderFragment renderFragment)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.RenderInExistingBatch(RenderQueueEntry renderQueueEntry)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.ProcessRenderQueue()
```


Attempting to use [Utility Base Component Classes from](https://docs.microsoft.com/en-us/aspnet/core/blazor/dependency-injection?view=aspnetcore-3.1#utility-base-component-classes-to-manage-a-di-scope)
results in

```
 Unhandled exception rendering component: A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbC
ontext. For more information on how to avoid threading issues with DbContext, see https://go.microsoft.com/fwlink/?linkid=2097913.
System.InvalidOperationException: A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbContext. For m
ore information on how to avoid threading issues with DbContext, see https://go.microsoft.com/fwlink/?linkid=2097913.
   at Microsoft.EntityFrameworkCore.Internal.ConcurrencyDetector.EnterCriticalSection()
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryingEnumerable`1.Enumerator.MoveNext()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load[TSource](IQueryable`1 source)
   at Microsoft.EntityFrameworkCore.Internal.EntityFinder`1.Load(INavigation navigation, InternalEntityEntry entry)
   at Microsoft.EntityFrameworkCore.ChangeTracking.NavigationEntry.Load()
   at Microsoft.EntityFrameworkCore.Internal.LazyLoader.Load(Object entity, String navigationName)
   at Microsoft.EntityFrameworkCore.Proxies.Internal.LazyLoadingInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.Proxies.WeatherForecastProxy.get_Description()
   at sample_use_proxy.Pages.WeatherDetails.BuildRenderTree(RenderTreeBuilder __builder)
   at Microsoft.AspNetCore.Components.ComponentBase.<.ctor>b__6_0(RenderTreeBuilder builder)
   at Microsoft.AspNetCore.Components.Rendering.ComponentState.RenderIntoBatch(RenderBatchBuilder batchBuilder, RenderFragment renderFragment)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.RenderInExistingBatch(RenderQueueEntry renderQueueEntry)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.ProcessRenderQueue()
fail: Microsoft.AspNetCore.Components.Server.Circuits.CircuitHost[111]
      Unhandled exception in circuit 'g-UHNPrYSoxbEeiMghgnHC9GR0NhK9-94MFsbk1jlZU'.
System.InvalidOperationException: A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbContext. For m
ore information on how to avoid threading issues with DbContext, see https://go.microsoft.com/fwlink/?linkid=2097913.
   at Microsoft.EntityFrameworkCore.Internal.ConcurrencyDetector.EnterCriticalSection()
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryingEnumerable`1.Enumerator.MoveNext()
   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Load[TSource](IQueryable`1 source)
   at Microsoft.EntityFrameworkCore.Internal.EntityFinder`1.Load(INavigation navigation, InternalEntityEntry entry)
   at Microsoft.EntityFrameworkCore.ChangeTracking.NavigationEntry.Load()
   at Microsoft.EntityFrameworkCore.Internal.LazyLoader.Load(Object entity, String navigationName)
   at Microsoft.EntityFrameworkCore.Proxies.Internal.LazyLoadingInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.Proxies.WeatherForecastProxy.get_Description()
   at sample_use_proxy.Pages.WeatherDetails.BuildRenderTree(RenderTreeBuilder __builder)
   at Microsoft.AspNetCore.Components.ComponentBase.<.ctor>b__6_0(RenderTreeBuilder builder)
   at Microsoft.AspNetCore.Components.Rendering.ComponentState.RenderIntoBatch(RenderBatchBuilder batchBuilder, RenderFragment renderFragment)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.RenderInExistingBatch(RenderQueueEntry renderQueueEntry)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.ProcessRenderQueue()
fail: Microsoft.AspNetCore.Components.Server.Circuits.CircuitHost[111]
      Unhandled exception in circuit 'g-UHNPrYSoxbEeiMghgnHC9GR0NhK9-94MFsbk1jlZU'.
System.InvalidOperationException: Invalid attempt to call ReadAsync when reader is closed.
   at Microsoft.Data.Common.ADP.ExceptionWithStackTrace(Exception e)
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.EntityFrameworkCore.Query.Internal.QueryingEnumerable`1.AsyncEnumerator.MoveNextAsync()
   at Microsoft.EntityFrameworkCore.Query.ShapedQueryCompilingExpressionVisitor.SingleOrDefaultAsync[TSource](IAsyncEnumerable`1 asyncEnumerable, CancellationToken cancellationToken)
   at Microsoft.EntityFrameworkCore.Query.ShapedQueryCompilingExpressionVisitor.SingleOrDefaultAsync[TSource](IAsyncEnumerable`1 asyncEnumerable, CancellationToken cancellationToken)
   at sample_use_proxy.Pages.WeatherList.OnParametersSet() in C:\devel\samples\sample_use_proxy_blazor\sample_use_proxy\Pages\WeatherList.razor:line 66
   at System.Threading.Tasks.Task.<>c.<ThrowAsync>b__139_0(Object state)
   at Microsoft.AspNetCore.Components.Rendering.RendererSynchronizationContext.ExecuteSynchronously(TaskCompletionSource`1 completion, SendOrPostCallback d, Object state)
   at Microsoft.AspNetCore.Components.Rendering.RendererSynchronizationContext.<>c.<.cctor>b__23_0(Object state)
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location where exception was thrown ---
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at Microsoft.AspNetCore.Components.Rendering.RendererSynchronizationContext.ExecuteBackground(WorkItem item)

Process finished with exit code 1.
```
