using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        public static void IsFalse(bool condition, string message = null)
        {
            if (condition)
                throw new Exception(message ?? "Assert.IsFalse failed.");
        }

        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    using NUnit.Framework;

    [TestFixture]
    public class PdfFileEditorDeleteTests
    {
        // Initialise with empty strings to satisfy non‑nullable warnings.
        private string _inputPdfPath = string.Empty;
        private string _outputPdfPath = string.Empty;

        [SetUp]
        public void SetUp()
        {
            _inputPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            _outputPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Pages.Add();
                doc.Save(_inputPdfPath);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_inputPdfPath))
                File.Delete(_inputPdfPath);
            if (File.Exists(_outputPdfPath))
                File.Delete(_outputPdfPath);
        }

        [Test]
        public void Delete_WithPageNumberBeyondDocumentLength_ShouldThrowException()
        {
            int[] pagesToDelete = new int[] { 5 };
            PdfFileEditor editor = new PdfFileEditor();

            Assert.Throws<Exception>(() =>
            {
                editor.Delete(_inputPdfPath, pagesToDelete, _outputPdfPath);
            });
        }

        [Test]
        public void TryDelete_WithPageNumberBeyondDocumentLength_ShouldReturnFalse()
        {
            int[] pagesToDelete = new int[] { 5 };
            PdfFileEditor editor = new PdfFileEditor();

            bool result = editor.TryDelete(_inputPdfPath, pagesToDelete, _outputPdfPath);

            Assert.IsFalse(result, "TryDelete should return false when page numbers are out of range.");
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the project is intended for unit‑test execution.
    }
}
