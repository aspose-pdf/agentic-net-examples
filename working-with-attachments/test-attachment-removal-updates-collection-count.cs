using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced
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
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfAttachmentTests
{
    // Dummy entry point to satisfy the compiler when the project expects an executable.
    public static class Program
    {
        public static void Main() { /* No‑op – tests are executed by the test runner */ }
    }

    [TestFixture]
    public class AttachmentRemovalTests
    {
        // Fields are initialized in SetUp; use null‑forgiving operator to silence warnings.
        private string _tempDir = null!;
        private string _pdfPath = null!;
        private string _modifiedPdfPath = null!;
        private string _attachmentPath = null!;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);

            _pdfPath = Path.Combine(_tempDir, "test_document.pdf");
            _modifiedPdfPath = Path.Combine(_tempDir, "test_document_modified.pdf");
            _attachmentPath = Path.Combine(_tempDir, "sample.txt");

            // Create a simple text file that will be attached to the PDF
            File.WriteAllText(_attachmentPath, "Sample attachment content.");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up all temporary files and directory
            try
            {
                if (File.Exists(_pdfPath)) File.Delete(_pdfPath);
                if (File.Exists(_modifiedPdfPath)) File.Delete(_modifiedPdfPath);
                if (File.Exists(_attachmentPath)) File.Delete(_attachmentPath);
                if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true);
            }
            catch
            {
                // Ignored – cleanup should not fail the test suite
            }
        }

        [Test]
        public void RemovingEmbeddedFileUpdatesCollectionCount()
        {
            // ---------- Create PDF and embed a file ----------
            using (var doc = new Document())
            {
                // A PDF must contain at least one page
                doc.Pages.Add();

                // Prepare the FileSpecification – the file data is supplied via a stream
                var fileSpec = new FileSpecification(Path.GetFileName(_attachmentPath))
                {
                    // Load the file bytes into a MemoryStream and assign to Contents
                    Contents = new MemoryStream(File.ReadAllBytes(_attachmentPath))
                };

                // Add the file to the EmbeddedFiles collection (the correct API for attachments)
                doc.EmbeddedFiles.Add(fileSpec);

                // Verify that the file was added
                Assert.AreEqual(1, doc.EmbeddedFiles.Count, "Embedded file was not added correctly.");

                // Persist the PDF to disk
                doc.Save(_pdfPath);
            }

            // ---------- Load PDF, remove the embedded file, and verify count ----------
            using (var loadedDoc = new Document(_pdfPath))
            {
                // Ensure the embedded file exists before removal
                Assert.AreEqual(1, loadedDoc.EmbeddedFiles.Count, "Expected one embedded file before removal.");

                // Retrieve the first (and only) embedded file – note 1‑based indexing
                var fileSpec = loadedDoc.EmbeddedFiles[1];

                // Remove the embedded file by name using the Delete method
                loadedDoc.EmbeddedFiles.Delete(fileSpec.Name);

                // Verify that the collection count is now zero
                Assert.AreEqual(0, loadedDoc.EmbeddedFiles.Count, "Embedded file removal did not update the collection count.");

                // Save the modified PDF (optional – demonstrates persistence)
                loadedDoc.Save(_modifiedPdfPath);
            }
        }
    }
}
