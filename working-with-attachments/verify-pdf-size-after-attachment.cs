using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void Greater<T>(T actual, T expected, string? message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
                throw new Exception(message ?? $"Assert.Greater failed. Expected > {expected}, but got {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AttachmentSizeTests
    {
        private const string SampleTextFile = "sample.txt";

        // Helper to create a simple text file used as attachment
        private void CreateSampleFile()
        {
            File.WriteAllText(SampleTextFile, "This is a sample attachment file.");
        }

        // Helper to delete temporary files safely
        private void DeleteIfExists(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                // ignore cleanup errors
            }
        }

        [Test]
        public void AddingFileAttachmentIncreasesPdfSize()
        {
            // Arrange: create a simple PDF with one blank page
            string originalPdfPath = Path.GetTempFileName();
            string attachedPdfPath = Path.GetTempFileName();

            CreateSampleFile();

            try
            {
                // Create and save the original PDF
                using (Document doc = new Document())
                {
                    // Add a blank page (Pages.Add() creates a new page)
                    doc.Pages.Add();
                    doc.Save(originalPdfPath); // save without any attachments
                }

                long originalSize = new FileInfo(originalPdfPath).Length;

                // Act: load the PDF, add a FileAttachmentAnnotation, and save to a new file
                using (Document doc = new Document(originalPdfPath))
                {
                    // Get the first page (1‑based indexing)
                    Page page = doc.Pages[1];

                    // Define the rectangle where the attachment icon will appear
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                    // Create a FileSpecification for the attachment file
                    using (FileStream fs = File.OpenRead(SampleTextFile))
                    {
                        FileSpecification fileSpec = new FileSpecification(fs, SampleTextFile);

                        // Create the FileAttachmentAnnotation
                        FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                        {
                            // Optional: add a description
                            Contents = "Sample attachment"
                        };

                        // Add the annotation to the page
                        page.Annotations.Add(attachment);
                    }

                    // Save the modified PDF to a new file
                    doc.Save(attachedPdfPath);
                }

                long attachedSize = new FileInfo(attachedPdfPath).Length;

                // Assert: the size after adding the attachment should be greater
                Assert.Greater(attachedSize, originalSize, "PDF size did not increase after adding a file attachment.");
            }
            finally
            {
                // Cleanup temporary files
                DeleteIfExists(originalPdfPath);
                DeleteIfExists(attachedPdfPath);
                DeleteIfExists(SampleTextFile);
            }
        }
    }
}

// Provide a minimal entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – tests are executed via the test runner.
    }
}