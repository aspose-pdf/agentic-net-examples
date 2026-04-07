using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using NUnit.Framework; // <-- added

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the real NUnit package is not referenced.
// -----------------------------------------------------------------------------
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
        // Greater assertion used in the test.
        public static void Greater<T>(T actual, T expected, string message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
                throw new Exception(message ?? $"Assert.Greater failed. Expected a value greater than <{expected}> but got <{actual}>.");
        }

        // Additional common assertions can be added here if needed.
    }
}

// -----------------------------------------------------------------------------
// Dummy entry point – required because the project is built as a console app.
// -----------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic needed; the unit tests are executed by the test runner.
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AttachmentSizeTests
    {
        private const string TempFolder = "TempTestFiles";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure a clean temporary directory for test files
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Cleanup after all tests have run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [Test]
        public void AddingFileAttachmentIncreasesPdfSize()
        {
            // Paths for the original and modified PDFs
            string originalPdfPath = Path.Combine(TempFolder, "original.pdf");
            string attachedPdfPath = Path.Combine(TempFolder, "withAttachment.pdf");
            string attachmentFilePath = Path.Combine(TempFolder, "sample.txt");

            // Create a simple text file to attach
            File.WriteAllText(attachmentFilePath, "This is a sample attachment.");

            // -----------------------------------------------------------------
            // Step 1: Create a minimal PDF (one blank page) and save it
            // -----------------------------------------------------------------
            using (Document doc = new Document())
            {
                // Add a blank page (Aspose.Pdf creates an empty document with no pages,
                // so we explicitly add one)
                doc.Pages.Add();

                // Save the PDF without any annotations
                doc.Save(originalPdfPath);
            }

            // Record the file size of the PDF without attachment
            long sizeWithoutAttachment = new FileInfo(originalPdfPath).Length;

            // -----------------------------------------------------------------
            // Step 2: Load the PDF, add a FileAttachmentAnnotation, and save
            // -----------------------------------------------------------------
            using (Document doc = new Document(originalPdfPath))
            {
                // Get the first (and only) page – Aspose.Pdf uses 1‑based indexing
                Page page = doc.Pages[1];

                // Define the rectangle for the annotation (position on the page)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create a FileSpecification that points to the file we want to embed
                FileSpecification fileSpec = new FileSpecification(attachmentFilePath);

                // Create the FileAttachmentAnnotation and add it to the page
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional: set a tooltip (Icon property is omitted because the enum is not available in this version)
                    Contents = "Sample attachment"
                };
                page.Annotations.Add(attachment);

                // Save the modified PDF
                doc.Save(attachedPdfPath);
            }

            // Record the file size after adding the attachment
            long sizeWithAttachment = new FileInfo(attachedPdfPath).Length;

            // -----------------------------------------------------------------
            // Assertion: the PDF with the attachment should be larger
            // -----------------------------------------------------------------
            Assert.Greater(sizeWithAttachment, sizeWithoutAttachment,
                $"Expected the PDF size to increase after adding an attachment. " +
                $"Before: {sizeWithoutAttachment} bytes, After: {sizeWithAttachment} bytes.");
        }
    }
}
