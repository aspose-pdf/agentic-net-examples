using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the real NUnit package is not referenced.
// Only the members used in this test are provided.
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
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
            {
                throw new Exception(message ?? "Assert.IsTrue failed.");
            }
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AttachmentTests
    {
        private const string TempFolder = "TempTestFiles";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure a clean temporary directory for the test run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Remove temporary files after all tests have finished
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [Test]
        public void AddAttachment_ShouldBePresentAfterSave()
        {
            // Arrange ---------------------------------------------------------
            string basePdfPath = Path.Combine(TempFolder, "base.pdf");
            string attachmentPath = Path.Combine(TempFolder, "sample.txt");
            string outputPdfPath = Path.Combine(TempFolder, "withAttachment.pdf");
            string attachmentContent = "This is a test attachment.";

            // Create a simple PDF document (one blank page)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(basePdfPath);
            }

            // Create a physical file that will be attached
            File.WriteAllText(attachmentPath, attachmentContent);

            // Act -------------------------------------------------------------
            // Add the attachment using PdfContentEditor (facade API)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(basePdfPath);
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");
            editor.Save(outputPdfPath); // Save creates the new PDF with the attachment

            // Assert ----------------------------------------------------------
            // Use PdfExtractor to verify that the attachment exists
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(outputPdfPath);
            extractor.ExtractAttachment(); // Extract all attachments

            IList<string> attachmentNames = extractor.GetAttachNames(); // Names of extracted attachments
            bool attachmentFound = false;
            foreach (string name in attachmentNames)
            {
                if (string.Equals(name, Path.GetFileName(attachmentPath), StringComparison.OrdinalIgnoreCase))
                {
                    attachmentFound = true;
                    break;
                }
            }

            Assert.IsTrue(attachmentFound, $"Attachment '{Path.GetFileName(attachmentPath)}' was not found in the saved PDF.");
        }
    }

    // Dummy entry point to satisfy the compiler when the project expects an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the test runner will invoke the tests directly.
        }
    }
}
