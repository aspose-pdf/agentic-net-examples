using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------
// Minimal NUnit stubs – used when the real NUnit package is not referenced
// ---------------------------------------------------------------------
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

        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    using NUnit.Framework;

    [TestFixture]
    public class PdfFileMendTests
    {
        private const string TempFolder = "TempTestFiles";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure temporary folder exists
            if (!Directory.Exists(TempFolder))
                Directory.CreateDirectory(TempFolder);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Cleanup temporary files after all tests
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        /// <summary>
        /// Creates a minimal PDF with a single blank page and returns its byte array.
        /// </summary>
        private byte[] CreateBlankPdf()
        {
            using (Document pdfDoc = new Document())
            {
                // Add a single blank page (Aspose.Pdf uses 1‑based indexing)
                pdfDoc.Pages.Add();

                // Save to a memory stream to obtain the raw bytes
                using (MemoryStream ms = new MemoryStream())
                {
                    pdfDoc.Save(ms);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Creates a tiny PNG image (10x10 red square) and returns the file path.
        /// </summary>
        private string CreateSampleImage()
        {
            string imagePath = Path.Combine(TempFolder, "sample.png");

            // Generate a simple bitmap using System.Drawing (Windows‑only GDI+)
            using (Bitmap bmp = new Bitmap(10, 10))
            {
                using (var gfx = Graphics.FromImage(bmp))
                {
                    // Fully qualify the Color to avoid ambiguity with Aspose.Pdf.Color
                    gfx.Clear(System.Drawing.Color.Red);
                }

                bmp.Save(imagePath, ImageFormat.Png);
            }

            return imagePath;
        }

        [Test]
        public void AddingImage_ShouldIncreasePdfByteSize()
        {
            // Arrange: create original PDF and sample image
            byte[] originalPdfBytes = CreateBlankPdf();
            string imageFile = CreateSampleImage();

            // Act: add the image using PdfFileMend
            byte[] modifiedPdfBytes;
            string outputPdfPath = Path.Combine(TempFolder, "modified.pdf");

            // Initialize PdfFileMend with source and destination files
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the original PDF from a memory stream
                using (MemoryStream sourceStream = new MemoryStream(originalPdfBytes))
                {
                    mend.BindPdf(sourceStream);
                    // Add image to page 1 at coordinates (10,10)-(100,100)
                    bool added = mend.AddImage(imageFile, 1, 10, 10, 100, 100);
                    Assert.IsTrue(added, "Image was not added successfully.");

                    // Save the modified PDF to a file
                    mend.Save(outputPdfPath);
                }
            }

            // Load the modified PDF bytes for comparison
            modifiedPdfBytes = File.ReadAllBytes(outputPdfPath);

            // Assert: the modified PDF should be larger than the original
            Assert.IsTrue(modifiedPdfBytes.Length > originalPdfBytes.Length,
                $"Modified PDF size ({modifiedPdfBytes.Length}) is not greater than original size ({originalPdfBytes.Length}).");
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main() { /* No‑op – tests are executed via the NUnit runner */ }
}
