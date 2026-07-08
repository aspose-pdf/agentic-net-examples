using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
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
    }
}

namespace AsposePdfTests
{
    public class AttachmentSizeTests
    {
        private const string TempFolder = "TempTestFiles";

        [SetUp]
        public void Setup()
        {
            // Ensure a clean temporary folder for each test run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [Test]
        public void AddingFileAttachmentIncreasesPdfSize()
        {
            // Paths for the PDFs
            string originalPdfPath = Path.Combine(TempFolder, "original.pdf");
            string attachedPdfPath = Path.Combine(TempFolder, "withAttachment.pdf");
            string attachmentFilePath = Path.Combine(TempFolder, "attachment.txt");

            // Create a simple text file to attach
            File.WriteAllText(attachmentFilePath, "This is a test attachment content.");

            // -----------------------------------------------------------------
            // Create a one‑page PDF and save it (original size)
            // -----------------------------------------------------------------
            using (Document doc = new Document())
            {
                // Add a blank page (Aspose.Pdf uses 1‑based indexing)
                doc.Pages.Add();

                // Save the baseline PDF
                doc.Save(originalPdfPath);
            }

            // Record the size of the PDF without attachment
            long sizeWithoutAttachment = new FileInfo(originalPdfPath).Length;

            // -----------------------------------------------------------------
            // Load the PDF, add a FileAttachmentAnnotation, and save again
            // -----------------------------------------------------------------
            using (Document doc = new Document(originalPdfPath))
            {
                // Create a FileSpecification that points to the attachment file
                FileSpecification fileSpec = new FileSpecification(attachmentFilePath);

                // Define the annotation rectangle (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create the attachment annotation on the first page
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
                {
                    // Optional: set a visible icon and a tooltip
                    Icon = FileIcon.Paperclip,
                    Contents = "Test attachment"
                };

                // Add the annotation to the page's annotation collection
                doc.Pages[1].Annotations.Add(attachment);

                // Save the PDF that now contains the attachment
                doc.Save(attachedPdfPath);
            }

            // Record the size of the PDF with attachment
            long sizeWithAttachment = new FileInfo(attachedPdfPath).Length;

            // -----------------------------------------------------------------
            // Verify that the file size increased after adding the attachment
            // -----------------------------------------------------------------
            Assert.IsTrue(sizeWithAttachment > sizeWithoutAttachment,
                $"Expected PDF size to increase after adding attachment. " +
                $"Before: {sizeWithoutAttachment} bytes, After: {sizeWithAttachment} bytes.");
        }

        [TearDown]
        public void Cleanup()
        {
            // Remove temporary files after test execution
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑type project
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – unit tests are executed by the test runner.
    }
}