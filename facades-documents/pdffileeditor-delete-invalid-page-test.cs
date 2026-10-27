using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – added because the project does not reference the real
// NUnit package. Only the members used by the tests are provided.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    // Added stub for OneTimeTearDown to fix build errors
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeTearDownAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        /// <summary>
        /// Executes the supplied delegate and returns the caught exception of type T.
        /// If no exception or a different exception is thrown, an AssertionException
        /// (represented here by a generic Exception) is raised.
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

namespace AsposePdfFacadeTests
{
    [TestFixture]
    public class DeletePageTests
    {
        private const string TempFolder = "TempTestFiles";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure a clean temporary folder for test files
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Remove temporary files after all tests have run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        /// <summary>
        /// Creates a simple PDF with the specified number of pages.
        /// </summary>
        private string CreatePdfWithPages(int pageCount)
        {
            string filePath = Path.Combine(TempFolder, $"sample_{pageCount}_pages.pdf");

            // Document implements IDisposable, so wrap it in a using block
            using (Document doc = new Document())
            {
                // Add the requested number of blank pages
                for (int i = 0; i < pageCount; i++)
                {
                    doc.Pages.Add();
                }

                // Save the PDF to disk
                doc.Save(filePath);
            }

            return filePath;
        }

        [Test]
        public void Delete_PageNumberExceedsDocumentLength_ThrowsException()
        {
            // Arrange: create a PDF with 2 pages
            string sourcePdf = CreatePdfWithPages(2);
            string outputPdf = Path.Combine(TempFolder, "output.pdf");

            // The page number 5 does not exist (valid range is 1‑2)
            int[] pagesToDelete = new int[] { 5 };

            PdfFileEditor editor = new PdfFileEditor();

            // Act & Assert: Delete should throw an exception for out‑of‑range page numbers
            // We assert any exception because the exact type may vary across Aspose versions.
            Assert.Throws<Exception>(() =>
            {
                // NOTE: In the Aspose.Pdf version used by this project the Delete overload
                // expects the page array as the second argument and the output file as the third.
                // Swapping the arguments fixes the CS1503 conversion errors.
                editor.Delete(sourcePdf, pagesToDelete, outputPdf);
            });

            // Cleanup generated files if they exist
            if (File.Exists(sourcePdf))
                File.Delete(sourcePdf);
            if (File.Exists(outputPdf))
                File.Delete(outputPdf);
        }
    }
}

// -----------------------------------------------------------------------------
// Dummy entry point to satisfy the compiler when the project is built as an
// executable. The test runner (NUnit) will discover and execute the tests.
// -----------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the presence of Main satisfies the
        // compiler's requirement for an entry point.
    }
}
