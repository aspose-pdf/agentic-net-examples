using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // <-- added to bring stub attributes into scope

// ---------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// ---------------------------------------------------------------------------
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
        /// <summary>
        /// Executes the supplied delegate and returns the caught exception of type T.
        /// Throws a generic Exception if no exception or a different exception is thrown.
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

[TestFixture]
public class PdfFileEditorDeleteTests
{
    private const string TempDir = "TempPdfTests";

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        Directory.CreateDirectory(TempDir);
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Directory.Delete(TempDir, true);
    }

    private string CreateSamplePdf(int pageCount, string fileName)
    {
        string path = Path.Combine(TempDir, fileName);
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
            {
                doc.Pages.Add();
            }
            doc.Save(path);
        }
        return path;
    }

    [Test]
    public void Delete_WithPageNumberBeyondLength_ShouldThrow()
    {
        // Arrange: create a 2‑page PDF
        string inputPath = CreateSamplePdf(2, "sample.pdf");
        string outputPath = Path.Combine(TempDir, "result.pdf");

        // Act & Assert: Delete should throw when page index exceeds document length
        PdfFileEditor editor = new PdfFileEditor();
        NUnit.Framework.Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            // Page numbers are 1‑based; 5 is out of range for a 2‑page document
            editor.Delete(inputPath, new int[] { 5 }, outputPath);
        });
    }
}

// ---------------------------------------------------------------------------
// Dummy entry point – required for projects that target an executable output.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic needed; the presence of Main satisfies the compiler.
    }
}
