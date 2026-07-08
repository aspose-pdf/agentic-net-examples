using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
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
    [TestFixture]
    public class PdfAnnotationEditorTests
    {
        // Helper method to create a PDF with a given number of text annotations.
        private string CreatePdfWithAnnotations(int annotationCount)
        {
            // Create a temporary file path.
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Create a new PDF document and add a single page.
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Add the requested number of text annotations.
                for (int i = 0; i < annotationCount; i++)
                {
                    // Fully qualify Rectangle to avoid ambiguity.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100 + i * 10, 500, 200 + i * 10, 550);
                    TextAnnotation txtAnn = new TextAnnotation(page, rect)
                    {
                        Title = $"Note {i + 1}",
                        Contents = $"Annotation {i + 1}",
                        Open = true,
                        Color = Aspose.Pdf.Color.Yellow
                    };
                    page.Annotations.Add(txtAnn);
                }

                // Save the PDF to the temporary file.
                doc.Save(tempPath);
            }

            return tempPath;
        }

        [Test]
        public void DeleteAnnotations_RemovesAllAnnotations()
        {
            // Arrange: create a PDF with 3 annotations.
            string sourcePdf = CreatePdfWithAnnotations(3);
            string outputPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "_clean.pdf");

            // Verify the source PDF actually contains the expected number of annotations.
            using (Document verifyDoc = new Document(sourcePdf))
            {
                int initialCount = verifyDoc.Pages[1].Annotations.Count;
                Assert.AreEqual(3, initialCount, "Source PDF should contain 3 annotations before deletion.");
            }

            // Act: use PdfAnnotationEditor to delete all annotations and save the result.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(sourcePdf);
                editor.DeleteAnnotations(); // Deletes all annotations in the document.
                editor.Save(outputPdf);
            }

            // Assert: the resulting PDF should have zero annotations.
            using (Document resultDoc = new Document(outputPdf))
            {
                int finalCount = resultDoc.Pages[1].Annotations.Count;
                Assert.AreEqual(0, finalCount, "All annotations should be removed after DeleteAnnotations.");
            }

            // Cleanup temporary files.
            File.Delete(sourcePdf);
            File.Delete(outputPdf);
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public class Program
    {
        public static void Main() { }
    }
}
