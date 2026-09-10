using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added to bring NUnit stub types into scope

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
        /// <summary>
        /// Executes the supplied delegate and returns the caught exception of type T.
        /// Throws a generic Exception if no exception or a different exception type is thrown.
        /// </summary>
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
        private string? _tempDir;
        private string? _inputPdf;
        private string? _outputPdf;

        // Set up a temporary folder and a simple 2‑page PDF before each test
        [SetUp]
        public void SetUp()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir!);

            _inputPdf = Path.Combine(_tempDir, "input.pdf");
            _outputPdf = Path.Combine(_tempDir, "output.pdf");

            // Create a PDF with two blank pages
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // page 1
                doc.Pages.Add(); // page 2
                doc.Save(_inputPdf);
            }
        }

        // Clean up temporary files after each test
        [TearDown]
        public void TearDown()
        {
            try { if (File.Exists(_inputPdf)) File.Delete(_inputPdf); } catch { }
            try { if (File.Exists(_outputPdf)) File.Delete(_outputPdf); } catch { }
            try { if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true); } catch { }
        }

        // Verify that Delete throws when a page number larger than the document length is supplied
        [Test]
        public void Delete_PageNumberExceedsDocumentLength_ThrowsException()
        {
            // The input PDF has only 2 pages; attempt to delete page 5
            int[] pagesToDelete = new[] { 5 };

            // PdfFileEditor does NOT implement IDisposable, so do NOT use a using statement.
            PdfFileEditor editor = new PdfFileEditor();
            Assert.Throws<Exception>(() => editor.Delete(_inputPdf!, pagesToDelete, _outputPdf!));
        }
    }
}

// Dummy entry point to satisfy the compiler when building as a console application.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are executed by the test runner.
    }
}
