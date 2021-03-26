using System;
using System.Collections.Generic;
using Xunit;

namespace EqualityComparerStuff
{
    
    public class UnitTest1
    {
        [Theory]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test1(Type comparer)
        {
            var a = new DemoObject();

            Assert.Equal(a, a, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }

        [Theory]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test2(Type comparer)
        {
            var a = new DemoObject();
            var b = new DemoObject();

            Assert.Equal(a, b, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }

        [Theory]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test3(Type comparer)
        {
            var a = new DemoObject { Name = "Bob" };
            var b = new DemoObject { Name = "Alice" };

            Assert.NotEqual(a, b, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }

        [Theory]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test4(Type comparer)
        {
            var a = new DemoObject { Name = "Bob" };
            var b = new DemoObject { Name = "Alice" };

            Assert.NotEqual(a, b, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }

        [Theory]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test5(Type comparer)
        {
            var a = new DemoObject { List = new List<DemoObject> { new DemoObject { Name = "Bob" } } };
            var b = new DemoObject { List = new List<DemoObject> { new DemoObject { Name = "Bob" } } };

            Assert.Equal(a, b, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }

        [Theory]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test6(Type comparer)
        {
            var a = new DemoObject { List = new List<DemoObject> { new DemoObject { Name = "Alice" } } };
            var b = new DemoObject { List = new List<DemoObject> { new DemoObject { Name = "Bob" } } };

            Assert.NotEqual(a, b, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }

        [Theory(Timeout = 500)]
        [InlineData(typeof(JsonComparer<DemoObject>))]
        [InlineData(typeof(NewtonsoftJsonComparer<DemoObject>))]
        [InlineData(typeof(TemplateObjectComparer<DemoObject>))]
        public void Test7(Type comparer)
        {
            var a = new DemoObject { List = new List<DemoObject>() };
            a.List.Add(a);

            var b = new DemoObject { List = new List<DemoObject>() };
            b.List.Add(b);

            Assert.Equal(a, b, Activator.CreateInstance(comparer) as IEqualityComparer<DemoObject>);
        }
    }
}
