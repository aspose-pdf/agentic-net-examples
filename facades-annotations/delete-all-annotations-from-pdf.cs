using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

    public static class Assert
    {
        // Allow null for the optional message parameter (nullable reference type)
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class PdfAnnotationEditorTests
    {
        private const string TempFolder = "TempTestFiles";

        [NUnit.Framework.OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure a clean temporary folder for test files
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [NUnit.Framework.Test]
        public void DeleteAnnotations_RemovesAllAnnotations()
        {
            // Arrange ---------------------------------------------------------
            // Create a simple PDF with a single text annotation
            string sourcePdfPath = Path.Combine(TempFolder, "source.pdf");
            using (Document doc = new Document())
            {
                // Add a blank page (Pages are 1‑based)
                doc.Pages.Add();

                // Create a text annotation on the first page
                // Fully qualify Rectangle to avoid ambiguity with System.Drawing
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation textAnn = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title    = "Test Note",
                    Contents = "This is a test annotation.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true
                };
                doc.Pages[1].Annotations.Add(textAnn);

                // Save the PDF to disk
                doc.Save(sourcePdfPath);
            }

            // Verify that the annotation exists before deletion
            using (Document verifyDoc = new Document(sourcePdfPath))
            {
                NUnit.Framework.Assert.AreEqual(1, verifyDoc.Pages[1].Annotations.Count,
                    "Pre‑condition failed: the source PDF should contain exactly one annotation.");
            }

            // Act -------------------------------------------------------------
            // Use PdfAnnotationEditor to delete all annotations
            string resultPdfPath = Path.Combine(TempFolder, "result.pdf");
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(sourcePdfPath);
                editor.DeleteAnnotations();               // Delete all annotations
                editor.Save(resultPdfPath);               // Save the modified PDF
            }

            // Assert ----------------------------------------------------------
            // Reload the resulting PDF and ensure no annotations remain
            using (Document resultDoc = new Document(resultPdfPath))
            {
                // Pages collection is 1‑based; check each page's annotation count
                for (int i = 1; i <= resultDoc.Pages.Count; i++)
                {
                    NUnit.Framework.Assert.AreEqual(0, resultDoc.Pages[i].Annotations.Count,
                        $"Page {i} should have no annotations after DeleteAnnotations.");
                }
            }
        }

        [NUnit.Framework.OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Clean up temporary files after all tests have run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }
    }
}

// Minimal entry point to satisfy the compiler for a console application
public class Program
{
    public static void Main(string[] args)
    {
        // The test runner (e.g., dotnet test) will discover and execute the tests.
        // No runtime logic is required here.
    }
}
