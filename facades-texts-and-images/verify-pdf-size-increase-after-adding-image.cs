using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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

        public static void AreNotEqual<T>(T notExpected, T actual, string? message = null)
        {
            if (object.Equals(notExpected, actual))
                throw new Exception(message ?? $"Assert.AreNotEqual failed. Both are <{actual}>.");
        }

        public static void Greater(long actual, long expected, string? message = null)
        {
            if (actual <= expected)
                throw new Exception(message ?? $"Assert.Greater failed. Actual:{actual} Expected greater than:{expected}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfImageModificationTests
    {
        // Minimal PNG (1x1 pixel, transparent) byte array
        private static readonly byte[] MinimalPng = new byte[]
        {
            0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
            0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
            0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
            0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
            0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
            0x54,0x78,0x9C,0x63,0x60,0x00,0x00,0x00,
            0x02,0x00,0x01,0xE2,0x21,0xBC,0x33,0x00,
            0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
            0x42,0x60,0x82
        };

        private string? _tempDirectory;
        private string? _originalPdfPath;
        private string? _imagePath;
        private string? _modifiedPdfPath;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test files
            _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDirectory!);

            // Paths for files
            _originalPdfPath = Path.Combine(_tempDirectory, "original.pdf");
            _imagePath = Path.Combine(_tempDirectory, "test.png");
            _modifiedPdfPath = Path.Combine(_tempDirectory, "modified.pdf");

            // Write the minimal PNG to disk
            File.WriteAllBytes(_imagePath, MinimalPng);

            // Create a blank PDF with a single page and save it
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // 1‑based indexing
                doc.Save(_originalPdfPath);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files and directory
            try
            {
                if (!string.IsNullOrEmpty(_tempDirectory) && Directory.Exists(_tempDirectory))
                {
                    Directory.Delete(_tempDirectory, true);
                }
            }
            catch
            {
                // Ignored – best effort cleanup
            }
        }

        [Test]
        public void AddingImageIncreasesPdfByteSize()
        {
            // Load original PDF into a Document instance
            using (Document originalDoc = new Document(_originalPdfPath!))
            {
                // Record original size
                long originalSize;
                using (MemoryStream ms = new MemoryStream())
                {
                    originalDoc.Save(ms);
                    originalSize = ms.Length;
                }

                // Use PdfFileMend to add an image to page 1
                using (PdfFileMend mender = new PdfFileMend(originalDoc))
                {
                    // Add the PNG image at coordinates (10,10) to (100,100)
                    bool added = mender.AddImage(_imagePath!, 1, 10f, 10f, 100f, 100f);
                    Assert.IsTrue(added, "Image should be added successfully.");
                    mender.Close(); // Ensure changes are flushed
                }

                // Save the modified document
                originalDoc.Save(_modifiedPdfPath!);

                // Get modified size
                long modifiedSize;
                using (MemoryStream ms = new MemoryStream())
                {
                    originalDoc.Save(ms);
                    modifiedSize = ms.Length;
                }

                // Verify that the size has increased
                Assert.AreNotEqual(originalSize, modifiedSize, "PDF size should change after adding an image.");
                Assert.Greater(modifiedSize, originalSize, "Modified PDF should be larger than the original.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main() { /* No‑op */ }
    }
}
