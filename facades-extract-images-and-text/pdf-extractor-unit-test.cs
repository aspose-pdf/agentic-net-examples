using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment
using NUnit.Framework; // <-- added

// Minimal NUnit stubs to allow compilation without the real NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Made the message parameter nullable to silence CS8625 warning
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfExtractorTests
    {
        private const string SampleText = "Sample Text for Extraction";

        private string CreateSamplePdf()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            string pdfPath = Path.Combine(tempDir, "sample.pdf");

            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Add a text fragment with known content
                TextFragment fragment = new TextFragment(SampleText);
                page.Paragraphs.Add(fragment);

                // Save the PDF
                doc.Save(pdfPath);
            }

            return pdfPath;
        }

        [Test]
        public void ExtractText_ReturnsExpectedText()
        {
            // Arrange: create a PDF with known text
            string pdfPath = CreateSamplePdf();

            // Act: extract text using PdfExtractor
            string extractedText;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();

                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    extractedText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            // Assert: the extracted text contains the sample text
            Assert.IsTrue(extractedText.Contains(SampleText), "Extracted text does not contain the expected content.");
        }
    }

    // Dummy entry point to satisfy the console‑app requirement of the project.
    // The test runner will discover and execute the tests; the Main method is never used.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – required for compilation when the project type expects an entry point.
        }
    }
}
