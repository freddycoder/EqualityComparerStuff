# EqualityComparerStuff
Some exploration in implementing the simplest possible implementation of IEqualityComparer\<T\> in C#.

## Goal
Compare complexe type without having to implement all the logic of Primitive types, Reference types, IEnumerable, etc.

## Result
After spending an evening on this, I believe that in dotnet core 3.1 the simplest way is to use newtonsoft to create an implementation like this :
```
public class NewtonsoftJsonComparer<T> : IEqualityComparer<T> where T : class
{
    private readonly JsonSerializerSettings _jsonSerializerOptions;

    public NewtonsoftJsonComparer()
    {
        _jsonSerializerOptions = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }

    public NewtonsoftJsonComparer(JsonSerializerSettings jsonSerializerOptions = null)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public bool Equals([AllowNull] T x, [AllowNull] T y)
    {
        return JsonConvert.SerializeObject(x, _jsonSerializerOptions) == JsonConvert.SerializeObject(y, _jsonSerializerOptions);
    }

    public int GetHashCode([DisallowNull] T obj)
    {
        return obj.GetHashCode();
    }
}
```

In Net 5.0, I believe that you can do the same using System.Text.Json to get the same thing done without installing any nuget.

Comparing complexe type is <i>complexe</i>, I understant that this implementation does not cover all case, and anyone using this should to.