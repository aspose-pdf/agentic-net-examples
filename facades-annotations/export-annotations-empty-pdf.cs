using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    // Bring the NUnit attributes into this namespace after the stub definition
    using NUnit.Framework;

    [TestFixture]
    public class ExportAnnotationsTests
    {
        [Test]
        public void ExportAnnotations_EmptyPdf_ShouldProduceValidXfdf()
        {
            // Create a PDF with a single blank page
            using (Document document = new Document())
            {
                Page page = document.Pages.Add();

                // Verify that the page contains no annotations
                Assert.AreEqual(0, page.Annotations.Count);

                // Export annotations to XFDF using a memory stream
                using (MemoryStream stream = new MemoryStream())
                {
                    document.ExportAnnotationsToXfdf(stream);
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string xfdfContent = reader.ReadToEnd();
                        // Basic validation: the XFDF root element should be present
                        Assert.IsTrue(xfdfContent.Contains("<xfdf"), "XFDF content does not contain the root element.");
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable
    public static class Program
    {
        public static void Main()
        {
            // No runtime logic required – the presence of Main satisfies the entry‑point requirement.
        }
    }
}