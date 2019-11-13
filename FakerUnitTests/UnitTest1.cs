using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakerLibrary;


namespace FakerUnitTests
{

    public class A
    {
        public B inA { get; set; }
    }

    public class B
    {
        public C inB { get; set; }
    }

    public class C
    {
        public A inC { get; set; }
    }




    public class Bar
    {
        public long a;
        private bool b;
        public List<Bar> listofBar;
        public Foo foo;
        public long BarProperty { get; set; }
    }

    public class Foo
    {
        public Bar bar;
    }


    public class ClassWithDifferentConstructors
    {
        public int a;
        public bool b { get; set; }
        private float f;
        public float publicf { get { return f; } }
        private string str = "Private";
        public string publicstr { get { return str; } }
        public string str2 = "public";
        public string publicstr2 { get { return str2; } }
        public ClassWithDifferentConstructors(int a, bool b)
        {
            this.a = a;
            this.b = b;
        }
        public ClassWithDifferentConstructors(bool b)
        {
            this.b = b;
        }
        private ClassWithDifferentConstructors(int a, bool b, float f, string str)
        {
            this.a = a;
            this.b = b;
            this.f = f;
            this.str = str;
        }
    }

    public class ListTest
    {
        public int a;
        public bool b;
    }



    class ClassWithNoPublicConstructors
    {
        private ClassWithNoPublicConstructors()
        {

        }
    }


    [TestClass]
    public class UnitTest1
    {
        private readonly Faker faker = new Faker();

        [TestMethod]
        public void ShouldFillIntWithNotDefaultValue()
        {
            int TestInt = faker.Create<int>();
            Assert.AreNotEqual(default(int), TestInt);
        }

        [TestMethod]
        public void ShouldAvoidUnsupportedType()
        {
            short s = faker.Create<short>();
            Assert.AreEqual(default(short), s);
        }

        [TestMethod]
        public void ShouldFillListWithDifferentValues()
        {
            List<long> list = faker.Create<List<long>>();
            Assert.IsNotNull(list);
            if (list.Count > 1)
            {
                Assert.AreNotEqual(list[0], list[1]);
            }
        }

        [TestMethod]
        public void ShouldAvoidRecursion()
        {
            A a = faker.Create<A>();
            Assert.IsNotNull(a);
            Assert.IsNotNull(a.inA);
            Assert.IsNotNull(a.inA.inB);
            Assert.IsNull(a.inA.inB.inC);
        }

        [TestMethod]
        public void ShouldReturnNullToNoPublicConstructors()
        {
            ClassWithNoPublicConstructors noPublicConstructors = faker.Create<ClassWithNoPublicConstructors>();
            Assert.IsNull(noPublicConstructors);
        }

        [TestMethod]
        public void ShouldFillObjectWithNotADefaultValuesAndAvoidRecursion()
        {
            Foo foo = faker.Create<Foo>();
            Assert.IsNotNull(foo.bar);
            Assert.IsNull(foo.bar.foo);
            Assert.AreNotEqual(default(long), foo.bar.a);
        }

        [TestMethod]
        public void ShouldFillObjectWithMaxConstructor()
        {
            ClassWithDifferentConstructors differentConstructors = faker.Create<ClassWithDifferentConstructors>();
            Assert.IsNotNull(differentConstructors);
            Assert.AreNotEqual(default(int), differentConstructors.a);
            Assert.AreNotEqual(default(bool), differentConstructors.b);
            Assert.AreEqual("Private", differentConstructors.publicstr);
            Assert.AreEqual(default(float), differentConstructors.publicf);
        }

        [TestMethod]
        public void ShouldFillListOfGenericType()
        {
            List<ListTest> lt = faker.Create<List<ListTest>>();
            Assert.IsNotNull(lt);

        }
    }
}
