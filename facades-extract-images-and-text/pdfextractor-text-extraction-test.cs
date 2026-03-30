using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
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
    [TestFixture]
    public class PdfExtractorTests
    {
        [Test]
        public void ExtractText_ReturnsExpectedText()
        {
            // Arrange: create a temporary PDF with known text
            string tempPdfPath = Path.Combine(Path.GetTempPath(), "sample.pdf");
            using (Document document = new Document())
            {
                Page page = document.Pages.Add();
                TextFragment textFragment = new TextFragment("Hello World");
                page.Paragraphs.Add(textFragment);
                document.Save(tempPdfPath);
            }

            // Act: extract text using PdfExtractor
            string extractedTextPath = Path.Combine(Path.GetTempPath(), "extracted.txt");
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(tempPdfPath);
            extractor.ExtractText();
            extractor.GetText(extractedTextPath);

            // Assert: verify the extracted text matches the original content
            string extractedContent = File.ReadAllText(extractedTextPath);
            Assert.AreEqual("Hello World", extractedContent.Trim());

            // Cleanup temporary files
            try
            {
                File.Delete(tempPdfPath);
                File.Delete(extractedTextPath);
            }
            catch (Exception)
            {
                // Ignored – cleanup failure should not affect test result
            }
        }
    }

    // Entry point required for compilation when the project is built as an executable.
    public class Program
    {
        public static void Main()
        {
            // No runtime logic needed; the presence of Main satisfies the compiler.
        }
    }
}