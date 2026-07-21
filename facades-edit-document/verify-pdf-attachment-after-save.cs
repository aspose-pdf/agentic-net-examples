using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// -----------------------------------------------------------------------------
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
    [TestFixture]
    public class AttachmentTests
    {
        private string? _tempFolder;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary folder for test files
            _tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempFolder);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files
            if (!string.IsNullOrEmpty(_tempFolder) && Directory.Exists(_tempFolder))
            {
                Directory.Delete(_tempFolder, true);
            }
        }

        [Test]
        public void Attachment_Should_Appear_After_Save()
        {
            // Arrange: paths for the original PDF, attachment file and the output PDF
            string originalPdfPath = Path.Combine(_tempFolder!, "original.pdf");
            string attachmentPath   = Path.Combine(_tempFolder!, "sample.txt");
            string outputPdfPath    = Path.Combine(_tempFolder!, "withAttachment.pdf");

            // Create a simple PDF with one blank page
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(originalPdfPath); // save the PDF
            }

            // Create a sample attachment file
            File.WriteAllText(attachmentPath, "This is a test attachment.");

            // Act: add the attachment using PdfContentEditor and save the result
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(originalPdfPath);
            editor.AddDocumentAttachment(attachmentPath, "Test attachment description");
            editor.Save(outputPdfPath);
            editor.Close(); // close the facade

            // Assert: extract attachments and verify the expected one exists
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(outputPdfPath);
            extractor.ExtractAttachment(); // extract all attachments
            IList<string> attachNames = extractor.GetAttachNames();

            bool attachmentFound = false;
            foreach (object nameObj in attachNames)
            {
                string? name = nameObj as string;
                if (!string.IsNullOrEmpty(name) && name.Equals(Path.GetFileName(attachmentPath), StringComparison.OrdinalIgnoreCase))
                {
                    attachmentFound = true;
                    break;
                }
            }

            Assert.IsTrue(attachmentFound, $"Attachment '{Path.GetFileName(attachmentPath)}' was not found in the PDF.");
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the project is intended for unit testing.
    }
}
