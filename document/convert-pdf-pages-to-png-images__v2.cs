using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace AsposePdfTests
{
    // Import the minimal NUnit stubs so the test can compile without the real NUnit package.
    using NUnit.Framework;

    // Minimal NUnit stubs defined inside the same namespace to avoid external dependencies.
    namespace NUnit.Framework
    {
        [AttributeUsage(AttributeTargets.Class)]
        public sealed class TestFixtureAttribute : Attribute { }

        [AttributeUsage(AttributeTargets.Method)]
        public sealed class TestAttribute : Attribute { }

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

        public class TestContext
        {
            private static readonly TestContext _current = new TestContext();
            public static TestContext Current => _current;

            // For the purpose of the test we treat the current directory as the test directory.
            public string TestDirectory => Directory.GetCurrentDirectory();
        }
    }

    [TestFixture]
    public class ConvertPagesToImagesTests
    {
        [Test]
        public void ConvertPagesToImages_GeneratesImageForEachPage()
        {
            // Path to an existing PDF file used for the test.
            // Ensure that this file exists in the test deployment folder.
            string inputPdfPath = Path.Combine(TestContext.Current.TestDirectory, "sample.pdf");
            Assert.IsTrue(File.Exists(inputPdfPath), $"Test PDF not found: {inputPdfPath}");

            // Create a temporary directory where the images will be written.
            string outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(outputDir);

            try
            {
                // Load the PDF document.
                using (Document doc = new Document(inputPdfPath))
                {
                    int pageCount = doc.Pages.Count;

                    // Render each page to a PNG image using the core Aspose.Pdf.Devices API.
                    for (int i = 1; i <= pageCount; i++)
                    {
                        var page = doc.Pages[i];
                        string imagePath = Path.Combine(outputDir, $"page_{i}.png");
                        using (var imageStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            // PngDevice renders a page to a PNG image.
                            var pngDevice = new PngDevice();
                            pngDevice.Process(page, imageStream);
                        }
                    }
                }

                // Determine how many image files were created.
                string[] imageFiles = Directory.GetFiles(outputDir, "*.png");
                int generatedImageCount = imageFiles.Length;

                // Get the expected page count from the source document.
                int expectedPageCount;
                using (Document doc = new Document(inputPdfPath))
                {
                    expectedPageCount = doc.Pages.Count;
                }

                // Verify that an image was generated for every page.
                Assert.AreEqual(expectedPageCount, generatedImageCount,
                    $"Expected {expectedPageCount} images, but found {generatedImageCount}.");
            }
            finally
            {
                // Clean up the temporary output directory.
                if (Directory.Exists(outputDir))
                {
                    Directory.Delete(outputDir, true);
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    public static class Program
    {
        public static void Main()
        {
            // No operation – the real verification is performed by the test above.
        }
    }
}
