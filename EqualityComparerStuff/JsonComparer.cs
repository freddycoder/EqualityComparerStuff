using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace EqualityComparerStuff
{
    public class JsonComparer<T> : IEqualityComparer<T> where T : class
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonComparer()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                // Sadly there is no option for hadling cyclic reference in dotnet core 3.1 but there is in .net 5
                // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-preserve-references?pivots=dotnet-core-3-1
            };
        }

        public JsonComparer(JsonSerializerOptions jsonSerializerOptions = null)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            return JsonSerializer.Serialize(x, _jsonSerializerOptions) == JsonSerializer.Serialize(y, _jsonSerializerOptions);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
