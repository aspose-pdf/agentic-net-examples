using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added reference to NUnit stubs

// Minimal NUnit stubs – used when the real NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeTearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        // Overload for int to match typical usage without generic type inference.
        public static void AreEqual(int expected, int actual, string? message = null) => AreEqual<int>(expected, actual, message);

        public static void Greater(int actual, int expected, string? message = null)
        {
            if (!(actual > expected))
                throw new Exception(message ?? $"Assert.Greater failed. Expected > {expected}, but was {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class ImageExtractionModeTests
    {
        private string _pdfPath;
        private string _imagePath;

        // Minimal 1x1 PNG (transparent) encoded in base64
        private const string Base64Png =
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/5+BAQAE/wJ" +
            "ZcKcAAAAASUVORK5CYII=";

        [OneTimeSetUp]
        public void SetUp()
        {
            // Create temporary folder
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            // Write PNG file
            _imagePath = Path.Combine(tempDir, "sample.png");
            byte[] pngBytes = Convert.FromBase64String(Base64Png);
            File.WriteAllBytes(_imagePath, pngBytes);

            // Create PDF with two images on the first page
            _pdfPath = Path.Combine(tempDir, "sample.pdf");
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // First image
                Aspose.Pdf.Image img1 = new Aspose.Pdf.Image
                {
                    File = _imagePath,
                    // Position the image
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                page.Paragraphs.Add(img1);

                // Second image (same file, different position)
                Aspose.Pdf.Image img2 = new Aspose.Pdf.Image
                {
                    File = _imagePath,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                page.Paragraphs.Add(img2);

                doc.Save(_pdfPath);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            try
            {
                if (File.Exists(_pdfPath)) File.Delete(_pdfPath);
                if (File.Exists(_imagePath)) File.Delete(_imagePath);
                string dir = Path.GetDirectoryName(_pdfPath);
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
            }
            catch
            {
                // Ignored – cleanup failure should not affect test results
            }
        }

        private int CountExtractedImages(ExtractImageMode mode)
        {
            int count = 0;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(_pdfPath);
                extractor.ExtractImageMode = mode;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    // Use a dummy stream; we only need to advance the iterator
                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractor.GetNextImage(ms);
                    }
                    count++;
                }
            }
            return count;
        }

        [Test]
        public void Verify_ImageExtractionMode_Influences_Count()
        {
            // Count images when extracting all defined resources
            int definedInResourcesCount = CountExtractedImages(ExtractImageMode.DefinedInResources);

            // Count images when extracting only actually used images
            int actuallyUsedCount = CountExtractedImages(ExtractImageMode.ActuallyUsed);

            // Both counts should be greater than zero (images are present)
            Assert.Greater(definedInResourcesCount, 0, "No images were extracted with DefinedInResources mode.");
            Assert.Greater(actuallyUsedCount, 0, "No images were extracted with ActuallyUsed mode.");

            // For this simple PDF the counts are expected to be equal
            Assert.AreEqual(definedInResourcesCount, actuallyUsedCount,
                "Image counts differ between extraction modes for a PDF where all images are used.");
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – tests are executed via the test runner.
    }
}
