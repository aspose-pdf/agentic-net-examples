using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
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

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    // Minimal TestContext implementation – provides the directory where the test runs.
    public sealed class TestContext
    {
        private TestContext() { }
        public static TestContext CurrentContext { get; } = new TestContext();
        public string TestDirectory { get; } = Directory.GetCurrentDirectory();
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
    [TestFixture]
    public class PdfConversionTests
    {
        [Test]
        public void ConvertPagesToImages_GeneratesImageForEachPage()
        {
            // Arrange: locate a sample PDF file (ensure it exists in the test directory)
            string inputPdfPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "sample.pdf");
            Assert.IsTrue(File.Exists(inputPdfPath), $"Input PDF not found at '{inputPdfPath}'.");

            // Create a temporary directory for the output images
            string outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(outputDir);

            // Act: load the PDF and convert each page to an image using PngDevice
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                int pageCount = pdfDocument.Pages.Count;
                Resolution resolution = new Resolution(150); // default resolution
                PngDevice pngDevice = new PngDevice(resolution);

                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    string imagePath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    }
                }

                // Assert: the number of generated image files matches the number of pages
                string[] generatedImages = Directory.GetFiles(outputDir, "page_*.png");
                Assert.AreEqual(pageCount, generatedImages.Length,
                    $"Expected {pageCount} images, but found {generatedImages.Length}.");
            }

            // Cleanup: remove the temporary output directory
            Directory.Delete(outputDir, true);
        }
    }
}

// -----------------------------------------------------------------------------
// Entry point required for a console‑type project. The test runner can invoke the
// tests via reflection; the Main method simply exists to satisfy the compiler.
// -----------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the presence of this method satisfies the CS5001 requirement.
    }
}
