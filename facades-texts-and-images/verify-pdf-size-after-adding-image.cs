using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class PdfFileMendTests
    {
        private const string SampleImagePath = "sample.jpg"; // ensure this image exists in test run folder

        // Helper to create a simple one‑page PDF in memory and return its byte array
        private static byte[] CreateBlankPdf()
        {
            using (Document doc = new Document())
            {
                // Add a single blank page (Aspose.Pdf uses 1‑based indexing)
                doc.Pages.Add();
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save without specifying SaveOptions – this writes a PDF
                    doc.Save(ms);
                    return ms.ToArray();
                }
            }
        }

        // Helper to add an image to a PDF using PdfFileMend and return the resulting byte array
        private static byte[] AddImageToPdf(byte[] pdfBytes)
        {
            // Write the original PDF to a temporary file because PdfFileMend works with file paths
            string inputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            string outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                File.WriteAllBytes(inputPath, pdfBytes);

                // Initialize PdfFileMend with input and output files
                PdfFileMend mend = new PdfFileMend(inputPath, outputPath);

                // Add the image to page 1 at coordinates (10,10)-(100,100)
                using (FileStream imgStream = File.OpenRead(SampleImagePath))
                {
                    // PdfFileMend.AddImage returns a bool indicating success
                    bool added = mend.AddImage(imgStream, 1, 10, 10, 100, 100);
                    NUnit.Framework.Assert.IsTrue(added, "Image was not added successfully.");
                }

                // Close the facade to flush changes
                mend.Close();

                // Read the modified PDF bytes
                return File.ReadAllBytes(outputPath);
            }
            finally
            {
                // Clean up temporary files
                if (File.Exists(inputPath)) File.Delete(inputPath);
                if (File.Exists(outputPath)) File.Delete(outputPath);
            }
        }

        [NUnit.Framework.Test]
        public void AddingImage_IncreasesPdfByteSize()
        {
            // Arrange: create a blank PDF
            byte[] originalPdf = CreateBlankPdf();

            // Act: add an image
            byte[] modifiedPdf = AddImageToPdf(originalPdf);

            // Assert: the modified PDF should be larger than the original
            NUnit.Framework.Assert.IsTrue(modifiedPdf.Length > originalPdf.Length,
                $"Modified PDF size ({modifiedPdf.Length}) is not greater than original size ({originalPdf.Length}).");
        }

        [NUnit.Framework.Test]
        public void AddingImage_DoesNotCorruptPdf()
        {
            // Arrange
            byte[] originalPdf = CreateBlankPdf();

            // Act
            byte[] modifiedPdf = AddImageToPdf(originalPdf);

            // Assert: the modified PDF should still be a valid PDF (has %PDF header)
            string header = System.Text.Encoding.ASCII.GetString(modifiedPdf, 0, Math.Min(4, modifiedPdf.Length));
            NUnit.Framework.Assert.AreEqual("%PDF", header, "Modified file does not start with PDF header, indicating corruption.");
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are discovered and run by the test runner.
        }
    }
}
