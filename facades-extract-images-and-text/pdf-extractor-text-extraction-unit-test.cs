using System;
using System.IO;
using System.Text;
using Aspose.Pdf;                     // Document, Page, TextFragment
using Aspose.Pdf.Text;                // TextFragment
using Aspose.Pdf.Facades;             // PdfExtractor

// Minimal NUnit stubs to allow compilation without the real NUnit package.
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
    public class PdfExtractorTests
    {
        // Initialise with a non‑null default to satisfy nullable analysis.
        private string _tempPdfPath = string.Empty;
        private const string SampleText = "Hello World from Aspose.Pdf!";

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            // Create a temporary PDF file with known text content.
            _tempPdfPath = Path.Combine(Path.GetTempPath(), $"sample_{Guid.NewGuid()}.pdf");

            // Use the recommended Document creation pattern (wrapped in using).
            using (Document doc = new Document())
            {
                // Add a page and a text fragment.
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment(SampleText);
                page.Paragraphs.Add(tf);

                // Save the PDF to the temporary location.
                doc.Save(_tempPdfPath);
            }
        }

        [NUnit.Framework.TearDown]
        public void TearDown()
        {
            // Clean up the temporary PDF file.
            if (File.Exists(_tempPdfPath))
            {
                File.Delete(_tempPdfPath);
            }
        }

        [NUnit.Framework.Test]
        public void ExtractText_ShouldReturnExactContent()
        {
            // Initialize the extractor and bind the PDF file.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(_tempPdfPath);

                // Perform text extraction.
                extractor.ExtractText();

                // Retrieve the extracted text into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    string extracted = Encoding.Unicode.GetString(ms.ToArray());

                    // Verify that the extracted text matches the original content.
                    NUnit.Framework.Assert.AreEqual(SampleText, extracted.Trim());
                }

                // Explicitly close the facade (optional, as using will dispose).
                extractor.Close();
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main() { /* No‑op – tests are executed by the test runner */ }
    }
}
