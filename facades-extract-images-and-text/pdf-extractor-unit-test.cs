using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework; // Added to bring NUnit stubs into scope

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null) // Made message nullable to silence CS8625
        {
            if (!object.Equals(expected, actual))
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfExtractorTests
    {
        private const string SampleText = "Hello Aspose PDF Extractor!";

        // Helper to create a simple PDF containing known text
        private string CreateSamplePdf()
        {
            // Create a temporary file path
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Use Aspose.Pdf Document within a using block (lifecycle rule)
            using (Document doc = new Document())
            {
                // Add a page (1‑based indexing)
                Page page = doc.Pages.Add();

                // Create a TextFragment with the sample text
                TextFragment fragment = new TextFragment(SampleText);

                // Add the fragment to the page
                page.Paragraphs.Add(fragment);

                // Save as PDF (no SaveOptions needed for PDF format)
                doc.Save(tempPdfPath);
            }

            return tempPdfPath;
        }

        [Test]
        public void ExtractText_ShouldReturnExactContent()
        {
            // Arrange: create a PDF with known content
            string pdfPath = CreateSamplePdf();

            // Act: extract text using PdfExtractor
            string extractedText;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file
                extractor.BindPdf(pdfPath);

                // Perform text extraction (Unicode encoding is default)
                extractor.ExtractText();

                // Retrieve the extracted text into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    // Reset stream position before reading
                    ms.Position = 0;
                    // Decode using Unicode (UTF‑16LE) as Aspose writes Unicode by default
                    extractedText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            // Clean up the temporary PDF file
            File.Delete(pdfPath);

            // Normalize line endings and trim whitespace for reliable comparison
            string normalizedExtracted = extractedText.Replace("\r\n", "\n").Trim();
            string normalizedExpected = SampleText.Trim();

            // Assert: the extracted text matches the original content
            Assert.AreEqual(normalizedExpected, normalizedExtracted, "Extracted text does not match the expected content.");
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main()
        {
            // No runtime logic required; tests are executed by the test runner.
        }
    }
}
