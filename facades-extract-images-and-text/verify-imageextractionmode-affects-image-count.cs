using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void GreaterOrEqual<T>(T actual, T expected, string message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) < 0)
                throw new Exception(message ?? $"Assert.GreaterOrEqual failed. Actual:<{actual}> is less than Expected:<{expected}>.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class ImageExtractionModeTests
    {
        private string _tempPdfPath;
        private string _tempImagePath;

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            // Create a simple bitmap to use as image source
            using (Bitmap bmp = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Blue);
                }

                // Save bitmap to a temporary file (used for visible image)
                _tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                bmp.Save(_tempImagePath, ImageFormat.Png);
            }

            // Create a PDF with one page
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Add a visible image to the page (this image will be actually used)
                Aspose.Pdf.Image visibleImg = new Aspose.Pdf.Image
                {
                    File = _tempImagePath,
                    // Position the image on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                page.Paragraphs.Add(visibleImg);

                // Add a hidden image directly to the page resources (defined but not used)
                using (FileStream imgStream = File.OpenRead(_tempImagePath))
                {
                    // This adds the image to the resources collection without placing it on the page
                    page.Resources.Images.Add(imgStream);
                }

                // Save the PDF to a temporary file
                _tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
                doc.Save(_tempPdfPath);
            }
        }

        [NUnit.Framework.TearDown]
        public void TearDown()
        {
            // Clean up temporary files
            if (File.Exists(_tempPdfPath))
                File.Delete(_tempPdfPath);
            if (File.Exists(_tempImagePath))
                File.Delete(_tempImagePath);
        }

        [NUnit.Framework.Test]
        public void ExtractImageMode_ShouldAffectExtractedImageCount()
        {
            // Extract using DefinedInResources mode (should retrieve both images)
            int countDefinedInResources;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(_tempPdfPath);
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                extractor.ExtractImage();

                countDefinedInResources = 0;
                while (extractor.HasNextImage())
                {
                    // Discard the extracted image; we only need the count
                    extractor.GetNextImage(Stream.Null);
                    countDefinedInResources++;
                }
            }

            // Extract using ActuallyUsed mode (should retrieve only the visible image)
            int countActuallyUsed;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(_tempPdfPath);
                extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
                extractor.ExtractImage();

                countActuallyUsed = 0;
                while (extractor.HasNextImage())
                {
                    extractor.GetNextImage(Stream.Null);
                    countActuallyUsed++;
                }
            }

            // Verify that both modes return a non‑negative count
            NUnit.Framework.Assert.GreaterOrEqual(countDefinedInResources, 0, "DefinedInResources count should be >= 0");
            NUnit.Framework.Assert.GreaterOrEqual(countActuallyUsed, 0, "ActuallyUsed count should be >= 0");

            // The DefinedInResources mode must return at least as many images as ActuallyUsed
            NUnit.Framework.Assert.GreaterOrEqual(countDefinedInResources, countActuallyUsed,
                "DefinedInResources should return equal or more images than ActuallyUsed");

            // In our constructed PDF we expect exactly 2 images in resources and 1 actually used
            NUnit.Framework.Assert.AreEqual(2, countDefinedInResources, "Expected 2 images when extracting all resources");
            NUnit.Framework.Assert.AreEqual(1, countActuallyUsed, "Expected 1 image when extracting only actually used images");
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    // In a real test project this class would not be needed, but it removes CS5001.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed by the test runner.
        }
    }
}
