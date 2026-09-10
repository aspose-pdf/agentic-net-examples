using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Generic Greater assertion used in the test.
        public static void Greater<T>(T actual, T expected, string message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
                throw new Exception(message ?? $"Assert.Greater failed. Expected > {expected}, but was {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AttachmentSizeTests
    {
        private const string TempFolder = "TempTestFiles";

        [SetUp]
        public void Setup()
        {
            // Ensure a clean temporary directory for each test run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [TearDown]
        public void Cleanup()
        {
            // Remove temporary files after tests
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [Test]
        public void AddingFileAttachmentIncreasesPdfSize()
        {
            // Paths for the PDFs before and after adding the attachment
            string originalPdfPath = Path.Combine(TempFolder, "original.pdf");
            string attachedPdfPath = Path.Combine(TempFolder, "withAttachment.pdf");

            // Create a small text file to attach (size ~100 bytes)
            string attachmentFilePath = Path.Combine(TempFolder, "sample.txt");
            File.WriteAllText(attachmentFilePath, new string('A', 100));

            // -----------------------------------------------------------------
            // Step 1: Create a simple PDF with a single blank page and save it
            // -----------------------------------------------------------------
            using (Document doc = new Document())
            {
                // Add a blank page (Aspose.Pdf uses 1‑based indexing)
                doc.Pages.Add();

                // Save the original PDF
                doc.Save(originalPdfPath);
            }

            // Record the file size of the original PDF
            long sizeBefore = new FileInfo(originalPdfPath).Length;

            // -----------------------------------------------------------------
            // Step 2: Load the PDF, add a FileAttachmentAnnotation, and save again
            // -----------------------------------------------------------------
            using (Document doc = new Document(originalPdfPath))
            {
                // Get the first (and only) page
                Page page = doc.Pages[1];

                // Define the rectangle where the attachment icon will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create a FileSpecification for the attachment file
                using (FileStream fs = File.OpenRead(attachmentFilePath))
                {
                    // Path.GetFileName can return null, but we know the path is a file, so use the null‑forgiving operator.
                    FileSpecification fileSpec = new FileSpecification(fs, Path.GetFileName(attachmentFilePath)!);

                    // Create the FileAttachmentAnnotation and add it to the page
                    FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                    page.Annotations.Add(attachment);
                }

                // Save the PDF that now contains the attachment
                doc.Save(attachedPdfPath);
            }

            // Record the file size after adding the attachment
            long sizeAfter = new FileInfo(attachedPdfPath).Length;

            // -----------------------------------------------------------------
            // Assertion: the PDF with the attachment should be larger
            // -----------------------------------------------------------------
            Assert.Greater(sizeAfter, sizeBefore, "PDF size did not increase after adding a file attachment.");
        }
    }
}

// Entry point required for a console‑type project.
public static class Program
{
    public static void Main() { /* No operation – tests are executed via NUnit */ }
}
