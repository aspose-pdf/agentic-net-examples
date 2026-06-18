using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// -----------------------------------------------------------------------------
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
        // A 1x1 pixel PNG image (transparent) encoded in base64.
        private static readonly byte[] PngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/5+BAQAE/wJ/6V8AAAAASUVORK5CYII=");

        private string CreateSamplePdf()
        {
            // Create a temporary PDF file with two images in the resources.
            // Only one of them is placed on the page.
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            using (Document doc = new Document())
            {
                // Add a page.
                Page page = doc.Pages.Add();

                // Add first image to resources (unused).
                using (MemoryStream msUnused = new MemoryStream(PngBytes))
                {
                    // The Add method returns the name of the image resource.
                    page.Resources.Images.Add(msUnused);
                }

                // Add second image to resources and place it on the page (used).
                using (MemoryStream msUsed = new MemoryStream(PngBytes))
                {
                    // Add to resources.
                    page.Resources.Images.Add(msUsed);
                    // Place the image on the page.
                    page.AddImage(msUsed, new Aspose.Pdf.Rectangle(100, 500, 300, 600));
                }

                // Save the PDF.
                doc.Save(tempPdfPath);
            }

            return tempPdfPath;
        }

        private int CountExtractedImages(PdfExtractor extractor)
        {
            int count = 0;
            while (extractor.HasNextImage())
            {
                // Extract to a memory stream; we don't need the actual file.
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextImage(ms);
                }
                count++;
            }
            return count;
        }

        [Test]
        public void ImageExtractionModeInfluencesCount()
        {
            // Arrange: create a PDF with one used and one unused image.
            string pdfPath = CreateSamplePdf();

            try
            {
                // Act & Assert for DefinedInResources (should count both images).
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                    extractor.ExtractImage();

                    int imageCount = CountExtractedImages(extractor);
                    Assert.AreEqual(2, imageCount, "DefinedInResources should extract all images defined in resources.");
                }

                // Act & Assert for ActuallyUsed (should count only the image placed on the page).
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
                    extractor.ExtractImage();

                    int imageCount = CountExtractedImages(extractor);
                    Assert.AreEqual(1, imageCount, "ActuallyUsed should extract only images that are shown on the page.");
                }
            }
            finally
            {
                // Clean up the temporary PDF file.
                if (File.Exists(pdfPath))
                {
                    File.Delete(pdfPath);
                }
            }
        }
    }

    // ---------------------------------------------------------------------
    // Minimal entry point required for a console‑type project.
    // ---------------------------------------------------------------------
    public class Program
    {
        public static void Main(string[] args)
        {
            // The project is primarily a test library; the Main method is
            // intentionally left empty to satisfy the compiler's entry‑point
            // requirement without affecting test execution.
        }
    }
}
