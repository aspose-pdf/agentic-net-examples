using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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
    [NUnit.Framework.TestFixture]
    public class PdfAnnotationEditorTests
    {
        // Helper creates a PDF with two text annotations on the first page.
        private static byte[] CreatePdfWithAnnotations()
        {
            using (Document doc = new Document())
            {
                // Add a blank page.
                Page page = doc.Pages.Add();

                // First annotation.
                Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
                TextAnnotation textAnn1 = new TextAnnotation(page, rect1)
                {
                    Title = "Note 1",
                    Contents = "First annotation",
                    Color = Aspose.Pdf.Color.Yellow
                };
                page.Annotations.Add(textAnn1);

                // Second annotation.
                Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
                TextAnnotation textAnn2 = new TextAnnotation(page, rect2)
                {
                    Title = "Note 2",
                    Contents = "Second annotation",
                    Color = Aspose.Pdf.Color.LightGray
                };
                page.Annotations.Add(textAnn2);

                // Save to a memory stream and return the bytes.
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms); // Save as PDF.
                    return ms.ToArray();
                }
            }
        }

        [NUnit.Framework.Test]
        public void DeleteAnnotations_RemovesAllAnnotations()
        {
            // Arrange: create PDF with known number of annotations.
            byte[] pdfBytes = CreatePdfWithAnnotations();

            // Load the PDF into a Document to verify initial count.
            int initialCount;
            using (Document sourceDoc = new Document(new MemoryStream(pdfBytes)))
            {
                // Annotation collections are 1‑based.
                initialCount = sourceDoc.Pages[1].Annotations.Count;
                NUnit.Framework.Assert.AreEqual(2, initialCount, "Setup failed: PDF should contain 2 annotations.");
            }

            // Act: bind the PDF with PdfAnnotationEditor and delete all annotations.
            byte[] resultPdf;
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind from a stream.
                editor.BindPdf(new MemoryStream(pdfBytes));

                // Delete all annotations.
                editor.DeleteAnnotations();

                // Save the modified PDF to a memory stream.
                using (MemoryStream outStream = new MemoryStream())
                {
                    editor.Save(outStream);
                    resultPdf = outStream.ToArray();
                }
            }

            // Assert: the resulting PDF should have zero annotations.
            using (Document resultDoc = new Document(new MemoryStream(resultPdf)))
            {
                int finalCount = resultDoc.Pages[1].Annotations.Count;
                NUnit.Framework.Assert.AreEqual(0, finalCount, "DeleteAnnotations should remove all annotations.");
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main() { }
}
