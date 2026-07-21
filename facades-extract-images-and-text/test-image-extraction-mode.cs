using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs – used when the NUnit package is not referenced.
// These must be defined in the global namespace (not nested inside another namespace)
// so that the using directive `using NUnit.Framework;` can resolve them.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // GreaterOrEqual for IComparable types (used in the test above).
        public static void GreaterOrEqual<T>(T actual, T expected, string? message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) < 0)
                throw new Exception(message ?? $"Assert.GreaterOrEqual failed. Expected >= {expected}, but got {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfExtractorTests
    {
        // Path to a PDF file that contains at least one image defined in resources
        // but not displayed on the page (e.g., an unused image). Adjust the path as needed.
        private const string SamplePdfPath = "sample.pdf";

        [Test]
        public void ExtractImageMode_ShouldAffectExtractedImageCount()
        {
            // Count images when extracting all resources
            int definedInResourcesCount = ExtractImagesCount(SamplePdfPath, ExtractImageMode.DefinedInResources);

            // Count images when extracting only actually used images
            int actuallyUsedCount = ExtractImagesCount(SamplePdfPath, ExtractImageMode.ActuallyUsed);

            // The count for DefinedInResources should be greater than or equal to the count for ActuallyUsed
            // (greater if the PDF contains unused image resources).
            Assert.GreaterOrEqual(definedInResourcesCount, actuallyUsedCount,
                "DefinedInResources count should be >= ActuallyUsed count.");
        }

        private int ExtractImagesCount(string pdfPath, ExtractImageMode mode)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"Test PDF not found: {pdfPath}");

            int imageCount = 0;

            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document to the extractor
                extractor.BindPdf(pdfPath);

                // Set the extraction mode
                extractor.ExtractImageMode = mode;

                // Perform the extraction
                extractor.ExtractImage();

                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the image into a memory stream (no file I/O needed for the test)
                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractor.GetNextImage(ms);
                        imageCount++;
                    }
                }
            }

            return imageCount;
        }
    }

    // Provide a dummy entry point so the project builds as a console application.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
            // This stub satisfies the compiler's requirement for an entry point.
        }
    }
}
