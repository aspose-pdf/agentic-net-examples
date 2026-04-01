using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.XfaConverter;

// Minimal NUnit stubs to allow compilation without the real NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfDemo
{
    class Program
    {
        static void Main()
        {
            // Create a simple PDF document (required lifecycle)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("output.pdf");
            }

            // Base URL containing spaces and a non‑ASCII character
            string rawBase = "http://example.com/dir with spaces/and?query=val&other=ä";
            Uri uri = new Uri(rawBase);

            // XfaParserOptions now requires a page size – provide a dummy A4 size.
            var pageSize = new SizeF(595f, 842f); // width & height in points
            XfaParserOptions options = new XfaParserOptions(pageSize);
            options.BasePath = uri;

            string escaped = options.BasePath.AbsoluteUri;
            Console.WriteLine("Escaped BasePath: " + escaped);
        }
    }

    [NUnit.Framework.TestFixture]
    public class XfaParserOptionsTests
    {
        [NUnit.Framework.Test]
        public void BasePath_Is_Escaped_When_Containing_Special_Characters()
        {
            // Arrange
            string rawBase = "http://example.com/dir with spaces/and?query=val&other=ä";
            Uri uri = new Uri(rawBase);
            var pageSize = new SizeF(595f, 842f); // any valid size
            XfaParserOptions options = new XfaParserOptions(pageSize);
            options.BasePath = uri;

            // Act
            string escaped = options.BasePath.AbsoluteUri;

            // Assert
            NUnit.Framework.Assert.IsTrue(escaped.Contains("%20"), "Space characters must be escaped as %20");
            NUnit.Framework.Assert.IsTrue(escaped.Contains("%C3%A4"), "The character 'ä' must be UTF‑8 escaped");
        }
    }
}
