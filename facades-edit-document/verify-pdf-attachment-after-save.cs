using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

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
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void IsNotNull(object obj, string message = null)
        {
            if (obj == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }

        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed. Condition is false.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AttachmentTests
    {
        private string _tempDir = null!; // initialized in SetUp
        private string _sourcePdf = null!;
        private string _attachmentFile = null!;
        private string _outputPdf = null!;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);

            // Paths for the source PDF, attachment, and output PDF
            _sourcePdf = Path.Combine(_tempDir, "source.pdf");
            _attachmentFile = Path.Combine(_tempDir, "sample.txt");
            _outputPdf = Path.Combine(_tempDir, "output.pdf");

            // Create a minimal PDF (one blank page)
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // 1‑based indexing, adds first page
                doc.Save(_sourcePdf); // Save inside using block
            }

            // Create a simple text file to be attached
            File.WriteAllText(_attachmentFile, "This is a test attachment.");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files and directory
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        [Test]
        public void Attachment_Should_Appear_After_Save()
        {
            // Add the attachment to the PDF using PdfContentEditor (facade API)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(_sourcePdf);
                // AddDocumentAttachment adds a file attachment without a visual annotation
                editor.AddDocumentAttachment(_attachmentFile, "Test attachment description");
                editor.Save(_outputPdf); // Save the modified PDF
            }

            // Verify that the attachment exists using PdfExtractor
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(_outputPdf);
                extractor.ExtractAttachment(); // Extract all attachments

                // Get the list of attachment names
                IList<string> attachNames = extractor.GetAttachNames();

                // Assert that at least one attachment is present
                Assert.IsNotNull(attachNames, "Attachment name list should not be null.");
                Assert.IsTrue(attachNames.Count > 0, "No attachments were found after saving the PDF.");

                // Optional: verify that the expected file name is present
                bool found = false;
                foreach (string name in attachNames)
                {
                    if (name.Equals(Path.GetFileName(_attachmentFile), StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found, $"Attachment '{Path.GetFileName(_attachmentFile)}' was not found in the PDF.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an
    // executable. In a real test project this would be unnecessary because the
    // test runner provides its own entry point.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the tests are executed by the NUnit runner.
        }
    }
}
