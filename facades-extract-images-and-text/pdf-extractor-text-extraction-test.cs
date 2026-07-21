using System;
using System.IO;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    // Provides a simple TestContext with the current directory as the test directory.
    public class TestContext
    {
        public static TestContext CurrentContext { get; } = new TestContext();
        public string TestDirectory { get; }
        private TestContext()
        {
            TestDirectory = Directory.GetCurrentDirectory();
        }
    }

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    using NUnit.Framework;

    [TestFixture]
    public class PdfExtractorTests
    {
        // Path to the sample PDF file used for testing.
        // Ensure that "sample.pdf" exists in the test project's output directory.
        private static readonly string SamplePdfPath = Path.Combine(
            TestContext.CurrentContext.TestDirectory, "sample.pdf");

        // Expected text content of the sample PDF.
        // Adjust this value to match the actual text inside "sample.pdf".
        private const string ExpectedText = "Hello World";

        [Test]
        public void ExtractText_ShouldReturnExpectedContent()
        {
            // Verify that the sample PDF file exists before proceeding.
            Assert.IsTrue(File.Exists(SamplePdfPath), $"Sample PDF not found at '{SamplePdfPath}'.");

            // Create a temporary directory for the extracted text file.
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            string extractedTextPath = Path.Combine(tempDir, "extracted.txt");

            try
            {
                // Use PdfExtractor within a using block to ensure proper disposal.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file to the extractor.
                    extractor.BindPdf(SamplePdfPath);

                    // Perform the text extraction.
                    extractor.ExtractText();

                    // Save the extracted text to a temporary file.
                    extractor.GetText(extractedTextPath);
                }

                // Read the extracted text from the file.
                string actualText = File.ReadAllText(extractedTextPath).Trim();

                // Verify that the extracted text matches the expected content.
                Assert.AreEqual(ExpectedText, actualText, "Extracted text does not match the expected value.");
            }
            finally
            {
                // Clean up temporary files and directory.
                if (File.Exists(extractedTextPath))
                {
                    File.Delete(extractedTextPath);
                }

                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main()
    {
        // No operation – the real work is performed by the unit tests.
    }
}
