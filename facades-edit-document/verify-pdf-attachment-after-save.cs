using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

namespace AsposePdfTests
{
    [TestFixture]
    public class AttachmentTests
    {
        private string? _tempDir; // made nullable to satisfy compiler warnings

        [SetUp]
        public void SetUp()
        {
            // Create a unique temporary directory for the test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up all files and the temporary directory
            if (!string.IsNullOrEmpty(_tempDir) && Directory.Exists(_tempDir))
            {
                foreach (var file in Directory.GetFiles(_tempDir))
                {
                    try { File.Delete(file); } catch { /* ignore */ }
                }
                try { Directory.Delete(_tempDir, true); } catch { /* ignore */ }
            }
        }

        [Test]
        public void Attachment_Should_Appear_After_Save()
        {
            // Paths for source PDF, attachment file and the resulting PDF
            string sourcePdfPath = Path.Combine(_tempDir!, "source.pdf");
            string attachmentPath = Path.Combine(_tempDir!, "sample.txt");
            string resultPdfPath = Path.Combine(_tempDir!, "result.pdf");

            // ------------------------------------------------------------
            // 1. Create a minimal PDF document (one blank page)
            // ------------------------------------------------------------
            using (Document srcDoc = new Document())
            {
                srcDoc.Pages.Add(); // add a blank page
                srcDoc.Save(sourcePdfPath); // save the source PDF
            }

            // ------------------------------------------------------------
            // 2. Create a simple attachment file
            // ------------------------------------------------------------
            File.WriteAllText(attachmentPath, "This is a test attachment.");

            // ------------------------------------------------------------
            // 3. Add the attachment to the PDF using PdfContentEditor (Facade)
            // ------------------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(sourcePdfPath); // load the source PDF
                // AddDocumentAttachment adds the file as a document attachment (no visual annotation)
                editor.AddDocumentAttachment(attachmentPath, "Test attachment description");
                editor.Save(resultPdfPath); // persist changes to a new file
            }

            // ------------------------------------------------------------
            // 4. Verify that the attachment exists using PdfExtractor
            // ------------------------------------------------------------
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(resultPdfPath); // load the PDF that should contain the attachment
                extractor.ExtractAttachment();    // extract all attachments (in memory)

                // GetAttachNames returns the list of attachment file names as IList<string>
                IList<string> names = extractor.GetAttachNames();

                // The test passes if at least one attachment is found and its name matches the original file
                Assert.IsNotNull(names, "Attachment name list should not be null.");
                Assert.IsTrue(names.Count > 0, "No attachments were found after saving the PDF.");

                // Verify the exact attachment name
                bool found = false;
                foreach (string name in names)
                {
                    if (name.Equals(Path.GetFileName(attachmentPath), StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }
                Assert.IsTrue(found, $"Attachment '{Path.GetFileName(attachmentPath)}' was not found in the PDF.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed by the test runner.
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// ---------------------------------------------------------------------------
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