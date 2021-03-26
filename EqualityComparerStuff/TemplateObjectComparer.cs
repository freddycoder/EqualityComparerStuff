using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EqualityComparerStuff
{
    public class TemplateObjectComparer<T> : IEqualityComparer<T> where T : class
    {
        private readonly HashSet<object> _referenceSetX;
        private readonly HashSet<object> _referenceSetY;

        public TemplateObjectComparer()
        {
            _referenceSetX = new HashSet<object>();
            _referenceSetY = new HashSet<object>();
        }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            _referenceSetX.Clear();
            _referenceSetY.Clear();

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            var isEqual = ObjectEqual(x, y);

            _referenceSetX.Clear();
            _referenceSetY.Clear();

            return isEqual;
        }

        private bool ObjectEqual(object x, object y)
        {
            var isEqual = true;

            foreach (var prop in typeof(T).GetProperties())
            {
                var xpropValue = prop.GetValue(x);
                var ypropValue = prop.GetValue(y);

                if (prop.PropertyType.IsPrimitive || prop.PropertyType.Equals(typeof(string)))
                {
                    isEqual = xpropValue == ypropValue;
                }
                else if (prop.PropertyType.GetInterfaces().Any(x => x.Equals(typeof(IEnumerable))))
                {
                    // TODO
                    // Looking to use this https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sequenceequal?view=net-5.0
                }
                else
                {
                    return ObjectEqual(xpropValue, ypropValue);
                }

                if (isEqual == false)
                {
                    break;
                }
            }

            return isEqual;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
