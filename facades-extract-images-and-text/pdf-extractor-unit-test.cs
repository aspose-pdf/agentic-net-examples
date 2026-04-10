using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

// Minimal MSTest stubs to allow compilation without the actual MSTest framework
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class PdfExtractorTests
    {
        // Path to a known sample PDF file that contains the text "Hello Aspose PDF!"
        private const string SamplePdfPath = @"TestData\Sample.pdf";

        // Expected text extracted from the sample PDF (exact match, including line breaks)
        private const string ExpectedText = "Hello Aspose PDF!";

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void ExtractText_ShouldReturnExpectedContent()
        {
            // Ensure the sample PDF exists before running the test
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(File.Exists(SamplePdfPath), $"Sample PDF not found at '{SamplePdfPath}'.");

            // Use PdfExtractor within a using block for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(SamplePdfPath);

                // Perform the text extraction
                extractor.ExtractText();

                // Retrieve the extracted text into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    ms.Position = 0; // Reset stream position before reading

                    // Convert the stream bytes to a string using Unicode encoding (default for ExtractText)
                    string extractedText = new StreamReader(ms, Encoding.Unicode).ReadToEnd();

                    // Trim possible trailing line breaks for a reliable comparison
                    extractedText = extractedText.Trim();

                    // Verify that the extracted text matches the expected content
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(ExpectedText, extractedText, "Extracted text does not match the expected value.");
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as a console application
public class Program
{
    public static void Main()
    {
        // No operation – the project is intended for unit testing.
    }
}
