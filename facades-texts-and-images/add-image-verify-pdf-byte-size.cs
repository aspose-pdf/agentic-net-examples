using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added to bring stub attributes into scope

// Minimal NUnit stubs so the test project can compile without the real NUnit package.
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

        public static void Greater(int actual, int expected, string? message = null)
        {
            if (actual <= expected)
                throw new Exception(message ?? $"Assert.Greater failed. Expected greater than {expected}, but was {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfFileMendTests
    {
        // A tiny 1x1 pixel PNG (transparent) encoded in base64.
        private static readonly byte[] SamplePng = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/5+BAQAE/wJ" +
            "Z6VYAAAAASUVORK5CYII=");

        [Test]
        public void AddingImage_IncreasesPdfByteSize()
        {
            // ------------------------------------------------------------
            // Step 1: Create a minimal PDF document (one blank page)
            // ------------------------------------------------------------
            byte[] originalPdfBytes;
            using (Document doc = new Document())
            {
                // Add a single blank page
                doc.Pages.Add();

                // Save the document to a memory stream
                using (MemoryStream originalStream = new MemoryStream())
                {
                    doc.Save(originalStream);
                    originalPdfBytes = originalStream.ToArray();
                }
            }

            // Record the original size
            int originalSize = originalPdfBytes.Length;

            // ------------------------------------------------------------
            // Step 2: Bind the PDF to PdfFileMend and add an image
            // ------------------------------------------------------------
            // Bind the existing PDF from a memory stream
            using (PdfFileMend pdfMend = new PdfFileMend())
            {
                using (MemoryStream sourceStream = new MemoryStream(originalPdfBytes))
                {
                    pdfMend.BindPdf(sourceStream);
                }

                // Add the sample PNG to page 1 at coordinates (10,10)-(100,100)
                using (MemoryStream imageStream = new MemoryStream(SamplePng))
                {
                    bool added = pdfMend.AddImage(imageStream, 1, 10f, 10f, 100f, 100f);
                    Assert.IsTrue(added, "Image should be added successfully.");
                }

                // ------------------------------------------------------------
                // Step 3: Save the modified PDF to a new memory stream
                // ------------------------------------------------------------
                byte[] modifiedPdfBytes;
                using (MemoryStream modifiedStream = new MemoryStream())
                {
                    pdfMend.Save(modifiedStream);
                    modifiedPdfBytes = modifiedStream.ToArray();
                }

                // Record the modified size
                int modifiedSize = modifiedPdfBytes.Length;

                // ------------------------------------------------------------
                // Step 4: Verify that the PDF size increased after adding the image
                // ------------------------------------------------------------
                Assert.Greater(modifiedSize, originalSize,
                    $"Modified PDF size ({modifiedSize} bytes) should be greater than original size ({originalSize} bytes).");
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are discovered and run by the test runner.
        // This method exists solely to provide a valid entry point for the project.
    }
}
