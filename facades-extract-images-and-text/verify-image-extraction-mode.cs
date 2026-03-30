using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
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
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class ImageExtractionModeTests
    {
        private const string PdfFileName = "test.pdf";
        private const string ImageFileName = "sample.png";

        [SetUp]
        public void SetUp()
        {
            // Create a simple 1x1 PNG image in the test directory
            byte[] pngBytes = Convert.FromBase64String(
                "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/5+BFwAE/wJ/6VfZAAAAAElFTkSuQmCC");
            using (FileStream imgStream = new FileStream(ImageFileName, FileMode.Create, FileAccess.Write))
            {
                imgStream.Write(pngBytes, 0, pngBytes.Length);
            }

            // Build a PDF with two images in resources – one placed on the page, one not placed
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Add image to resources without placing it (defined but not actually used)
                using (FileStream resStream = new FileStream(ImageFileName, FileMode.Open, FileAccess.Read))
                {
                    page.Resources.Images.Add(resStream);
                }

                // Add the same image as a visible element on the page (actually used)
                Aspose.Pdf.Image visibleImage = new Aspose.Pdf.Image();
                visibleImage.File = ImageFileName;
                page.Paragraphs.Add(visibleImage);

                doc.Save(PdfFileName);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(PdfFileName))
            {
                File.Delete(PdfFileName);
            }
            if (File.Exists(ImageFileName))
            {
                File.Delete(ImageFileName);
            }
        }

        [Test]
        public void TestExtractImageModeCounts()
        {
            // Count images when extracting all defined resources
            int definedCount;
            using (PdfExtractor extractorDefined = new PdfExtractor())
            {
                extractorDefined.BindPdf(PdfFileName);
                extractorDefined.ExtractImageMode = ExtractImageMode.DefinedInResources;
                extractorDefined.ExtractImage();
                definedCount = 0;
                while (extractorDefined.HasNextImage())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractorDefined.GetNextImage(ms);
                        definedCount++;
                    }
                }
            }

            // Count images when extracting only actually used images
            int usedCount;
            using (PdfExtractor extractorUsed = new PdfExtractor())
            {
                extractorUsed.BindPdf(PdfFileName);
                extractorUsed.ExtractImageMode = ExtractImageMode.ActuallyUsed;
                extractorUsed.ExtractImage();
                usedCount = 0;
                while (extractorUsed.HasNextImage())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractorUsed.GetNextImage(ms);
                        usedCount++;
                    }
                }
            }

            // Verify that the mode influences the count as expected
            Assert.AreEqual(2, definedCount, "DefinedInResources should return two images (one used, one unused).");
            Assert.AreEqual(1, usedCount, "ActuallyUsed should return only the image that appears on the page.");
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main() { }
    }
}
