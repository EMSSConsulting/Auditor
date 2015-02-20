# Auditor
**Request logging and error handling framework for ASP.NET vNext**

Auditor provides a set of useful abstract middleware platforms, attributes and helper methods which simplify implementation of request logging and error handling within an ASP.NET vNext application.

Using Auditor is incredibly simple, just add it to your `project.json` file's `dependencies` section...
```json
{
  "dependencies": {
    "Auditor": "1.0.0"
  }
}
```

And then load it up in your application's `Configure` method.

```csharp
public override async Task Configure(IApplicationBuilder builder)
{
  builder.UseRouteLogger<MyRouteLogger>().UseErrorReporter<MyErrorReporter>();
  builder.UseMvc().UseNotFound<MyNotFoundHandler>();
}
```
