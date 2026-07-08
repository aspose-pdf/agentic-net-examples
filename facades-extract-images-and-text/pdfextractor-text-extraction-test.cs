using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

// Minimal NUnit stubs to allow compilation without the real NUnit package
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
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class PdfExtractorTests
    {
        private const string SampleText = "Sample text for extraction";

        // Helper method to create a temporary PDF containing known text
        private string CreateSamplePdf()
        {
            string tempPdfPath = Path.Combine(Path.GetTempPath(), $"sample_{Guid.NewGuid()}.pdf");

            // Create a new PDF document and add a page with the sample text
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                TextFragment fragment = new TextFragment(SampleText);
                page.Paragraphs.Add(fragment);
                doc.Save(tempPdfPath);
            }

            return tempPdfPath;
        }

        [NUnit.Framework.Test]
        public void ExtractText_ShouldReturnExpectedContent()
        {
            // Arrange: generate a PDF with known content
            string pdfPath = CreateSamplePdf();

            try
            {
                // Act: extract text using PdfExtractor
                string extractedText;
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractText();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractor.GetText(ms);
                        extractedText = Encoding.Unicode.GetString(ms.ToArray()).Trim();
                    }
                }

                // Assert: the extracted text matches the original sample text
                NUnit.Framework.Assert.AreEqual(SampleText, extractedText);
            }
            finally
            {
                // Clean up the temporary PDF file
                if (File.Exists(pdfPath))
                {
                    File.Delete(pdfPath);
                }
            }
        }
    }

    // Required entry point for a console application project
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the test runner will discover and execute tests.
        }
    }
}
