using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the real NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        // Simple implementation of Throws<T> used in the test
        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                // Expected exception type was thrown – return it
                return ex;
            }
            catch (Exception ex)
            {
                // Wrong exception type – re‑throw a descriptive exception
                throw new Exception($"Assert.Throws failed. Expected exception of type {typeof(T)} but got {ex.GetType()}.", ex);
            }

            // No exception was thrown – fail the assertion
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

[TestFixture]
public class PageDeleteTests
{
    private const string TestPdf = "test.pdf";

    [SetUp]
    public void Setup()
    {
        // Create a PDF with two pages and save it
        using (Document doc = new Document())
        {
            Page page1 = doc.Pages.Add();
            Page page2 = doc.Pages.Add();
            // Save the document to a simple file name
            doc.Save(TestPdf);
        }
    }

    [TearDown]
    public void Cleanup()
    {
        if (File.Exists(TestPdf))
        {
            File.Delete(TestPdf);
        }
    }

    [Test]
    public void Delete_PageNumberExceedsLength_ThrowsArgumentOutOfRangeException()
    {
        using (Document doc = new Document(TestPdf))
        {
            // The document contains 2 pages; attempting to delete page 5 should throw
            Assert.Throws<ArgumentOutOfRangeException>(delegate { doc.Pages.Delete(5); });
        }
    }
}

// Dummy entry point to satisfy the C# compiler when building as an executable.
public static class Program
{
    public static void Main()
    {
        // No operation – tests are executed by the test runner.
    }
}