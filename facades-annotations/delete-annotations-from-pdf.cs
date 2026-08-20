using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Allow null for the optional message in a nullable‑aware project.
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class PdfAnnotationEditorTests
    {
        // Helper to create a simple PDF with a given number of text annotations
        private static string CreatePdfWithAnnotations(int annotationCount)
        {
            // Create a temporary file path for the source PDF
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Create a new PDF document and add a single page
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Add the requested number of TextAnnotations
                for (int i = 0; i < annotationCount; i++)
                {
                    // Fully qualify Rectangle to avoid ambiguity with System.Drawing
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100 + i * 10, 500 + i * 10, 200 + i * 10, 550 + i * 10);
                    TextAnnotation txtAnn = new TextAnnotation(page, rect)
                    {
                        Title = $"Note {i + 1}",
                        Contents = $"This is annotation {i + 1}",
                        Color = Aspose.Pdf.Color.Yellow,
                        Open = true
                    };
                    page.Annotations.Add(txtAnn);
                }

                // Save the PDF to the temporary location
                doc.Save(tempPath);
            }

            return tempPath;
        }

        [NUnit.Framework.Test]
        public void DeleteAnnotations_RemovesAllAnnotations()
        {
            // Arrange: create a PDF with 3 annotations
            string sourcePdf = CreatePdfWithAnnotations(3);
            string outputPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "_out.pdf");

            // Verify initial annotation count using a Document instance
            int initialCount;
            using (Document verifyDoc = new Document(sourcePdf))
            {
                // Annotations collection uses 1‑based indexing; Count property gives total number
                initialCount = verifyDoc.Pages[1].Annotations.Count;
            }
            NUnit.Framework.Assert.AreEqual(3, initialCount, "Initial PDF should contain 3 annotations.");

            // Act: delete all annotations via PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(sourcePdf);
                editor.DeleteAnnotations(); // removes all annotations
                editor.Save(outputPdf);
            }

            // Assert: the resulting PDF should have zero annotations
            int finalCount;
            using (Document resultDoc = new Document(outputPdf))
            {
                finalCount = resultDoc.Pages[1].Annotations.Count;
            }
            NUnit.Framework.Assert.AreEqual(0, finalCount, "All annotations should have been removed.");

            // Cleanup temporary files
            File.Delete(sourcePdf);
            File.Delete(outputPdf);
        }
    }
}

// Dummy entry point to satisfy the compiler when the project expects an executable.
public static class Program
{
    public static void Main()
    {
        // No runtime logic required – tests are discovered and run by the test runner.
    }
}
