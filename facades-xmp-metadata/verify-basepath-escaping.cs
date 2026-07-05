using System;
using System.Drawing; // for SizeF
using Aspose.Pdf.XfaConverter; // XfaParserOptions resides here

// Minimal NUnit stubs to allow compilation without the NUnit package.
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
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class BasePathEscapingTests
    {
        [NUnit.Framework.Test]
        public void BasePath_ShouldBeEscaped_WhenContainingSpecialCharacters()
        {
            // Arrange: a URL that contains spaces and non‑ASCII characters.
            string rawUrl = "http://example.com/space folder/file.html?param=val ue&other=äöü";

            // Act: create a Uri (Aspose.Pdf.XfaParserOptions.BasePath is of type Uri).
            // The Uri constructor automatically escapes illegal characters.
            Uri uri = new Uri(rawUrl, UriKind.Absolute);

            // XfaParserOptions requires a page size in its constructor.
            var pageSize = new SizeF(595f, 842f); // A4 size in points (portrait)
            XfaParserOptions options = new XfaParserOptions(pageSize) { BasePath = uri };

            // Assert: the stored BasePath must be the escaped absolute URI.
            // Expected escaped representation:
            //   spaces -> %20
            //   non‑ASCII characters -> UTF‑8 percent‑encoding
            string expectedEscaped = "http://example.com/space%20folder/file.html?param=val%20ue&other=%C3%A4%C3%B6%C3%BC";
            NUnit.Framework.Assert.AreEqual(expectedEscaped, options.BasePath.AbsoluteUri,
                "BasePath was not correctly escaped.");
        }
    }
}

// Dummy entry point to satisfy the console‑application requirement of the build.
public static class Program
{
    public static void Main() { /* No‑op – unit tests are discovered via reflection */ }
}