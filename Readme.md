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
