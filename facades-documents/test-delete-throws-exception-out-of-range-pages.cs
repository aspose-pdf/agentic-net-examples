using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
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

    public delegate void TestDelegate();

    public static class Assert
    {
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

        // Set up a simple PDF with two pages before each test
        [SetUp]
        public void SetUp()
        {
            // Create temporary file paths
            _inputPdfPath = Path.Combine(Path.GetTempPath(), $"input_{Guid.NewGuid()}.pdf");
            _outputPdfPath = Path.Combine(Path.GetTempPath(), $"output_{Guid.NewGuid()}.pdf");

            // Create a PDF document with two blank pages
            using (Document doc = new Document())
            {
                // Add first page
                doc.Pages.Add();
                // Add second page
                doc.Pages.Add();

                // Save the document (required lifecycle rule: use Document.Save)
                doc.Save(_inputPdfPath);
            }
        }

        // Clean up temporary files after each test
        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(_inputPdfPath) && File.Exists(_inputPdfPath))
                File.Delete(_inputPdfPath);
            if (!string.IsNullOrEmpty(_outputPdfPath) && File.Exists(_outputPdfPath))
                File.Delete(_outputPdfPath);
        }

        // Test that Delete throws when page numbers exceed the document length
        [Test]
        public void Delete_WithOutOfRangePageNumbers_ShouldThrowException()
        {
            // Arrange: request deletion of page 5 (document only has 2 pages)
            int[] pagesToDelete = new[] { 5 };

            // Act & Assert: Delete should throw an exception (ArgumentException or similar)
            PdfFileEditor editor = new PdfFileEditor();

            // The Delete method (not TryDelete) is expected to throw on failure
            Assert.Throws<Exception>(() =>
            {
                // This call uses the Delete overload that throws on error
                editor.Delete(_inputPdfPath!, pagesToDelete, _outputPdfPath!);
            });
        }
    }
}

// Provide an entry point so the project builds as a console application.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the presence of Main satisfies the compiler.
    }
}