using System;
using System.Drawing; // for SizeF
using Aspose.Pdf.Facades;               // Facade namespace (required by task)
using Aspose.Pdf.XfaConverter;          // XFA parser options namespace

// Minimal NUnit stubs – added because the project does not reference the real NUnit package.
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
            // Arrange: a raw URL that includes spaces and a non‑ASCII character.
            const string rawUrl = "http://example.com/space folder/file name.html?query=param&other=ä";

            // Create a Uri instance – the constructor does NOT escape the characters.
            Uri unescapedUri = new Uri(rawUrl, UriKind.Absolute);

            // Act: assign the Uri to XfaParserOptions.BasePath.
            // XfaParserOptions requires a page size argument in its constructor.
            var pageSize = new SizeF(595f, 842f); // A4 size in points (width, height)
            XfaParserOptions options = new XfaParserOptions(pageSize);
            options.BasePath = unescapedUri;

            // The BasePath property returns the same Uri instance.
            // Its AbsoluteUri property provides the correctly escaped representation.
            string actualEscaped = options.BasePath.AbsoluteUri;

            // Expected escaped form: spaces become %20 and 'ä' becomes %C3%A4.
            const string expectedEscaped = "http://example.com/space%20folder/file%20name.html?query=param&other=%C3%A4";

            // Assert: the escaped URI matches the expectation.
            NUnit.Framework.Assert.AreEqual(expectedEscaped, actualEscaped);
        }
    }

    // Dummy entry point so the project can compile as an executable.
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are discovered/executed by the test runner.
        }
    }
}
