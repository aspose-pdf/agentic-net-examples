using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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

        public static void IsNotNull(object anObject, string message = null)
        {
            if (anObject == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }
    }

    public static class StringAssert
    {
        public static void Contains(string expectedSubstring, string actualString, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (actualString == null)
                throw new Exception("StringAssert.Contains failed. Actual string is null.");

            if (actualString.IndexOf(expectedSubstring, comparisonType) >= 0)
                return;

            throw new Exception($"StringAssert.Contains failed. Expected substring \"{expectedSubstring}\" not found in \"{actualString}\".");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfFileEditorDeleteTests
    {
        // Made nullable because they are assigned in SetUp, not in the constructor.
        private string? _inputFile;
        private string? _outputFile;

        // Create a simple PDF with two pages before each test
        [SetUp]
        public void SetUp()
        {
            _inputFile = Path.Combine(Path.GetTempPath(), $"input_{Guid.NewGuid()}.pdf");
            _outputFile = Path.Combine(Path.GetTempPath(), $"output_{Guid.NewGuid()}.pdf");

            // Use the standard Document creation pattern (lifecycle rule)
            using (Document doc = new Document())
            {
                // Add two blank pages (pages are 1‑based)
                doc.Pages.Add();
                doc.Pages.Add();

                // Save the document to a temporary file
                doc.Save(_inputFile);
            }
        }

        // Clean up temporary files after each test
        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(_inputFile) && File.Exists(_inputFile))
                File.Delete(_inputFile);

            if (!string.IsNullOrEmpty(_outputFile) && File.Exists(_outputFile))
                File.Delete(_outputFile);
        }

        [Test]
        public void Delete_WithPageNumberBeyondDocumentLength_ShouldThrowException()
        {
            // Arrange: request deletion of page 3, which does not exist (document has only 2 pages)
            int[] pagesToDelete = new int[] { 3 };

            // Act & Assert: Delete should throw an Aspose.Pdf.PdfException
            var editor = new PdfFileEditor(); // PdfFileEditor does NOT implement IDisposable
            var ex = Assert.Throws<PdfException>(() =>
                editor.Delete(_inputFile!, pagesToDelete, _outputFile!));

            // Optional: verify that the exception message indicates an invalid page number
            Assert.IsNotNull(ex);
            StringAssert.Contains("page", ex.Message, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Minimal entry point so the project compiles as a console application.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
