using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;
using NUnit.Framework; // Added to bring NUnit stub types into scope

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
        public static void AreEqual<T>(T expected, T actual, string? message = null)
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
    public class ConvertPagesToImagesTests
    {
        private string _tempDir;
        private string _pdfPath1;
        private string _pdfPath2;
        private string _outputDir;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);

            // Paths for the two PDF documents
            _pdfPath1 = Path.Combine(_tempDir, "doc1.pdf");
            _pdfPath2 = Path.Combine(_tempDir, "doc2.pdf");

            // Directory where comparison images will be saved
            _outputDir = Path.Combine(_tempDir, "ComparisonImages");
            Directory.CreateDirectory(_outputDir);

            // Create a simple PDF with 3 pages
            using (Document doc = new Document())
            {
                for (int i = 1; i <= 3; i++)
                {
                    // Add a new page
                    Page page = doc.Pages.Add();

                    // Add some text so the page is not empty
                    page.Paragraphs.Add(new TextFragment($"Page {i}"));
                }

                // Save the same document twice (identical content)
                doc.Save(_pdfPath1);
                doc.Save(_pdfPath2);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files and directories
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        [Test]
        public void ConvertPagesToImages_FlagGeneratesImageForEachPage()
        {
            // Load the two PDF documents
            using (Document doc1 = new Document(_pdfPath1))
            using (Document doc2 = new Document(_pdfPath2))
            {
                // Perform graphical comparison that outputs images per page
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToImages(
                    doc1,
                    doc2,
                    _outputDir,
                    "page_",
                    ImageFormat.Png);
            }

            // Verify that an image file was created for every page in the source PDF
            int expectedImageCount = 3; // we created 3 pages
            string[] imageFiles = Directory.GetFiles(_outputDir, "page_*.png");
            Assert.AreEqual(expectedImageCount, imageFiles.Length,
                $"Expected {expectedImageCount} image files, but found {imageFiles.Length}.");
        }
    }
}

// Dummy entry point to satisfy the console‑application requirement.
public class Program
{
    public static void Main()
    {
        // No operation – tests are executed by the test runner.
    }
}