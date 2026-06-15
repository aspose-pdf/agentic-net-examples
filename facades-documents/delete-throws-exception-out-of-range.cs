using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added to bring stubbed NUnit types into scope

// Minimal NUnit stubs to allow compilation without the real NUnit package
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

    public delegate void TestDelegate();

    public static class Assert
    {
        // Generic Throws<T> used in the test
        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex);
            }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfFileEditorDeleteTests
    {
        private string? _inputPdfPath;
        private string? _outputPdfPath;

        // Create a simple PDF with two pages for testing
        [OneTimeSetUp]
        public void CreateTestDocument()
        {
            // Ensure Aspose.Pdf can write to the file system
            _inputPdfPath = Path.Combine(Path.GetTempPath(), $"TestDoc_{Guid.NewGuid()}.pdf");
            _outputPdfPath = Path.Combine(Path.GetTempPath(), $"TestResult_{Guid.NewGuid()}.pdf");

            // Using statement guarantees deterministic disposal of the Document
            using (Document doc = new Document())
            {
                // Add two blank pages
                doc.Pages.Add();
                doc.Pages.Add();

                // Save the document to a temporary location
                doc.Save(_inputPdfPath);
            }
        }

        // Clean up temporary files after all tests have run
        [OneTimeTearDown]
        public void Cleanup()
        {
            if (_inputPdfPath != null && File.Exists(_inputPdfPath))
                File.Delete(_inputPdfPath);
            if (_outputPdfPath != null && File.Exists(_outputPdfPath))
                File.Delete(_outputPdfPath);
        }

        // Verify that Delete throws when a page number exceeds the document length
        [Test]
        public void Delete_WithPageNumberBeyondDocumentLength_ShouldThrowException()
        {
            // Arrange
            PdfFileEditor editor = new PdfFileEditor();

            // Act & Assert
            // The document has only 2 pages; attempting to delete page 5 should raise an exception.
            Assert.Throws<Exception>(delegate
            {
                // Delete method throws on failure (unlike TryDelete which returns false)
                editor.Delete(_inputPdfPath!, new int[] { 5 }, _outputPdfPath!);
            });
        }
    }
}

// Dummy entry point to satisfy the compiler for an executable project.
public static class Program
{
    public static void Main() { }
}
