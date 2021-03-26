using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EqualityComparerStuff
{
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
}
