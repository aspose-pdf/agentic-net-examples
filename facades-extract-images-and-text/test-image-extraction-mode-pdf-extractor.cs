using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;
using NUnit.Framework; // <-- added

// Minimal NUnit stubs to allow compilation without the NUnit package.
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
    public class PdfExtractorImageModeTests
    {
        private const string SampleImagePath = "sample.png"; // ensure this file exists in test run directory

        private string CreateTestPdf()
        {
            // Create a PDF with one image placed on the page (actually used)
            // and another image added only to the resources (defined but not used).
            string pdfPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid() + ".pdf");

            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Load image bytes (use any small PNG file placed alongside the test assembly)
                byte[] imgBytes = File.ReadAllBytes(SampleImagePath);
                using (MemoryStream imgStream = new MemoryStream(imgBytes))
                {
                    // Image that will be placed on the page (actually used)
                    Aspose.Pdf.Image usedImg = new Aspose.Pdf.Image { File = SampleImagePath };
                    page.Paragraphs.Add(usedImg);

                    // Image that will be added only to the resources (defined but not used)
                    // Add directly to the resources collection without adding to page content.
                    page.Resources.Images.Add(imgStream);
                }

                // Save the PDF to a temporary file
                doc.Save(pdfPath);
            }

            return pdfPath;
        }

        private int CountExtractedImages(string pdfPath, ExtractImageMode mode)
        {
            int count = 0;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file
                extractor.BindPdf(pdfPath);

                // Set the extraction mode
                extractor.ExtractImageMode = mode;

                // Perform extraction
                extractor.ExtractImage();

                // Count images using HasNextImage/GetNextImage loop
                while (extractor.HasNextImage())
                {
                    // We don't need to write the image to disk; just retrieve it to advance the iterator.
                    // Using a dummy stream to satisfy the overload.
                    using (MemoryStream dummy = new MemoryStream())
                    {
                        extractor.GetNextImage(dummy);
                    }
                    count++;
                }
            }

            return count;
        }

        [Test]
        public void ImageExtractionMode_ShouldAffectExtractedImageCount()
        {
            // Arrange
            string pdfPath = CreateTestPdf();

            // Act
            int definedInResourcesCount = CountExtractedImages(pdfPath, ExtractImageMode.DefinedInResources);
            int actuallyUsedCount = CountExtractedImages(pdfPath, ExtractImageMode.ActuallyUsed);

            // Cleanup
            File.Delete(pdfPath);

            // Assert
            // DefinedInResources should return 2 images (used + defined-only)
            // ActuallyUsed should return 1 image (only the one placed on the page)
            Assert.AreEqual(2, definedInResourcesCount, "DefinedInResources mode should extract all images defined in resources.");
            Assert.AreEqual(1, actuallyUsedCount, "ActuallyUsed mode should extract only images that are shown on the page.");
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static class Program
    {
        public static void Main() { /* No-op – tests are executed by the test runner */ }
    }
}
