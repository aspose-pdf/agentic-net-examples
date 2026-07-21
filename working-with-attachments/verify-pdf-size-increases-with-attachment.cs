using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void Greater(long actual, long expected, string message = null)
        {
            if (actual <= expected)
                throw new Exception(message ?? $"Assert.Greater failed. Expected greater than {expected}, but got {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class AttachmentSizeTests
    {
        // Helper to create a minimal PDF with a single blank page
        private static Document CreateBlankPdf()
        {
            // Document constructor creates an empty PDF
            Document doc = new Document();
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();
            return doc;
        }

        // Helper to create a temporary file that will be attached
        private static string CreateTempAttachmentFile()
        {
            string tempPath = Path.GetTempFileName();
            // Write some identifiable content – size matters for the test
            File.WriteAllText(tempPath, "This is a test attachment file.");
            return tempPath;
        }

        [NUnit.Framework.Test]
        public void AddingFileAttachmentIncreasesPdfSize()
        {
            // Arrange: create a blank PDF and record its size
            using (Document doc = CreateBlankPdf())
            {
                // Save the original PDF to a memory stream to measure size without touching the file system
                using (MemoryStream originalStream = new MemoryStream())
                {
                    doc.Save(originalStream);
                    long originalSize = originalStream.Length;

                    // Act: add a file attachment annotation
                    string attachmentPath = CreateTempAttachmentFile();
                    try
                    {
                        // Prepare the file specification for the attachment using the file path (constructor that accepts a string)
                        FileSpecification fileSpec = new FileSpecification(attachmentPath);
                        // Define a rectangle for the annotation (position on the page)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
                        // Create the annotation on the first page
                        FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
                        {
                            // Optional: set a tooltip (Icon property is omitted because the enum does not exist in this version)
                            Contents = "Test attachment"
                        };
                        // Add the annotation to the page
                        doc.Pages[1].Annotations.Add(attachment);

                        // Save the modified PDF to another memory stream
                        using (MemoryStream modifiedStream = new MemoryStream())
                        {
                            doc.Save(modifiedStream);
                            long modifiedSize = modifiedStream.Length;

                            // Assert: the modified PDF must be larger than the original
                            NUnit.Framework.Assert.Greater(modifiedSize, originalSize,
                                $"Modified PDF size ({modifiedSize} bytes) should be greater than original size ({originalSize} bytes).");
                        }
                    }
                    finally
                    {
                        // Clean up the temporary attachment file
                        if (File.Exists(attachmentPath))
                            File.Delete(attachmentPath);
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable
    public class Program
    {
        public static void Main() { }
    }
}
