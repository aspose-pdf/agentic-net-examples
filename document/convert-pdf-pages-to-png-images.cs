using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using NUnit.Framework; // Added to bring stubbed NUnit types into scope

// Minimal NUnit stubs to allow compilation without the NUnit package.
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
    [TestFixture]
    public class ConvertPagesToImagesTests
    {
        // Path to a sample PDF used for testing.
        // In a real test environment this file should be placed in the test project's output directory.
        private const string SamplePdfPath = "sample.pdf";

        // Temporary directory where generated images will be stored.
        private string _outputDir = string.Empty;

        [SetUp]
        public void SetUp()
        {
            // Create a unique temporary folder for each test run.
            _outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_outputDir);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up generated files after the test.
            if (Directory.Exists(_outputDir))
            {
                Directory.Delete(_outputDir, true);
            }
        }

        [Test]
        public void ConvertPagesToImages_FlagGeneratesImageForEveryPage()
        {
            // Verify that the sample PDF exists.
            Assert.IsTrue(File.Exists(SamplePdfPath), $"Test PDF not found: {SamplePdfPath}");

            // Load the PDF document.
            using (Document pdfDocument = new Document(SamplePdfPath))
            {
                // Create a resolution (300 DPI) for high‑quality images.
                Resolution resolution = new Resolution(300);

                // Initialize the PNG device with the chosen resolution.
                PngDevice pngDevice = new PngDevice(resolution);

                // Convert each page to a PNG file.
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    string imagePath = Path.Combine(_outputDir, $"page_{pageNumber}.png");
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    }
                }

                // Assert that an image file was created for every page.
                string[] generatedFiles = Directory.GetFiles(_outputDir, "page_*.png");
                Assert.AreEqual(pdfDocument.Pages.Count, generatedFiles.Length,
                    "Number of generated images does not match number of PDF pages.");

                // Additional sanity check: each file should exist and have a non‑zero size.
                foreach (string file in generatedFiles)
                {
                    FileInfo info = new FileInfo(file);
                    Assert.IsTrue(info.Exists, $"Image file missing: {file}");
                    Assert.IsTrue(info.Length > 0, $"Image file is empty: {file}");
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main() { /* No‑op */ }
}