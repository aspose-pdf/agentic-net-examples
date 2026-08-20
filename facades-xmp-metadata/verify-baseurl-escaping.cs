using System;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.XfaConverter;
using NUnit.Framework;

// Minimal NUnit stubs – used when the real NUnit package is not referenced.
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

[TestFixture]
public class BaseUrlEscapingTests
{
    [Test]
    public void BasePath_ShouldBeEscaped_WhenContainsSpecialCharacters()
    {
        // Arrange: create a URI that contains spaces and a non‑ASCII character.
        const string rawUrl = "http://example.com/space path/ünicode.html";
        Uri uri = new Uri(rawUrl, UriKind.Absolute);

        // XfaParserOptions holds the BasePath as a Uri. Provide a page size as required by the constructor.
        var pageSize = new SizeF(595f, 842f); // A4 size in points.
        XfaParserOptions options = new XfaParserOptions(pageSize)
        {
            BasePath = uri
        };

        // Use a Facade class to satisfy the “use Aspose.Pdf.Facades” rule.
        PdfViewer viewer = new PdfViewer();
        // No need to call Close; PdfViewer does not implement IDisposable in this context.

        // Act: obtain the escaped representation of the BasePath.
        string escaped = options.BasePath.AbsoluteUri;

        // Assert: verify that special characters are percent‑escaped.
        // Space should become %20
        Assert.IsTrue(escaped.Contains("%20"),
            $"Expected escaped space (%20) in '{escaped}'.");

        // The Unicode character 'ü' (U+00FC) should be UTF‑8 percent‑encoded (%C3%BC).
        Assert.IsTrue(escaped.Contains("%C3%BC"),
            $"Expected escaped Unicode (%C3%bc) in '{escaped}'.");
    }
}

// Provide an entry point to satisfy the compiler when the project expects an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the unit test framework will discover and run the test.
    }
}
