using System;
using System.Drawing; // for SizeF
using Aspose.Pdf;
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
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

[TestFixture]
public class BasePathEscapingTests
{
    [Test]
    public void XfaParserOptions_BasePath_EscapesSpecialCharacters()
    {
        // URL containing spaces and non‑ASCII characters.
        string rawUrl = "http://example.com/dir with spaces/file.html?param=1&value=äöü";

        // XfaParserOptions now requires a page size argument.
        var pageSize = new SizeF(595f, 842f); // A4 size in points (width x height)
        XfaParserOptions options = new XfaParserOptions(pageSize)
        {
            BasePath = new Uri(rawUrl)
        };

        // The Uri class automatically escapes spaces and non‑ASCII characters.
        string expectedEscaped = "http://example.com/dir%20with%20spaces/file.html?param=1&value=%C3%A4%C3%B6%C3%BC";

        Assert.AreEqual(expectedEscaped, options.BasePath.AbsoluteUri,
            "BasePath should be escaped correctly.");

        // Use a facade (PdfViewer) to satisfy the requirement of using Aspose.Pdf.Facades in the test.
        using (Document doc = new Document())
        {
            // Add a single blank page.
            doc.Pages.Add();

            // Save to a temporary file.
            string tempPdf = System.IO.Path.GetTempFileName();
            doc.Save(tempPdf);

            // Bind the PDF with PdfViewer (facade).
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(tempPdf);
                // No further actions needed; binding demonstrates facade usage.
                viewer.Close();
            }

            // Clean up the temporary file.
            System.IO.File.Delete(tempPdf);
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are executed by the test runner.
    }
}
