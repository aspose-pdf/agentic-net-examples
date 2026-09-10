using System;
using System.IO;
using System.Drawing; // needed for PdfContentEditor.CreateText overload
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

    public static class Assert
    {
        // Made the message parameter nullable to silence CS8625 warning
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
    public class TextAnnotationPositionTests
    {
        // Expected rectangle for the annotation (coordinates in points)
        // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
        // We want a rectangle 50x50 points positioned at (100,200)
        private static readonly Aspose.Pdf.Rectangle ExpectedRect = new Aspose.Pdf.Rectangle(100, 200, 150, 250);

        [NUnit.Framework.Test]
        public void TextAnnotation_ShouldBePlacedAtExpectedCoordinates()
        {
            // 1. Create a simple one‑page PDF in memory
            using (Document sourceDoc = new Document())
            {
                sourceDoc.Pages.Add(); // add a blank page

                // Save the source PDF to a memory stream (no file I/O)
                using (MemoryStream sourceStream = new MemoryStream())
                {
                    sourceDoc.Save(sourceStream);
                    sourceStream.Position = 0; // reset for reading

                    // 2. Use PdfContentEditor (a Facade) to add a text annotation
                    using (PdfContentEditor editor = new PdfContentEditor())
                    {
                        // Bind the PDF from the memory stream
                        editor.BindPdf(sourceStream);

                        // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle for the overload
                        var drawingRect = new System.Drawing.Rectangle(
                            (int)ExpectedRect.LLX,
                            (int)ExpectedRect.LLY,
                            (int)ExpectedRect.Width,
                            (int)ExpectedRect.Height);

                        // Create a text (sticky‑note) annotation at the expected rectangle on page 1
                        editor.CreateText(
                            drawingRect,               // rectangle defining position (System.Drawing.Rectangle)
                            "Test Title",               // annotation title
                            "Test contents",            // annotation contents
                            true,                       // open flag
                            "Note",                     // icon name
                            1);                         // page number (1‑based)

                        // Save the modified PDF to another memory stream
                        using (MemoryStream resultStream = new MemoryStream())
                        {
                            editor.Save(resultStream);
                            resultStream.Position = 0; // reset for reading

                            // 3. Load the resulting PDF with the core API to inspect the annotation
                            using (Document resultDoc = new Document(resultStream))
                            {
                                // Retrieve the first page (1‑based indexing)
                                Page page = resultDoc.Pages[1];

                                // Ensure there is exactly one annotation
                                NUnit.Framework.Assert.AreEqual(1, page.Annotations.Count, "Expected exactly one annotation on the page.");

                                // Get the annotation and cast to TextAnnotation
                                Annotation ann = page.Annotations[1]; // annotation collections are 1‑based
                                NUnit.Framework.Assert.AreEqual(typeof(TextAnnotation), ann.GetType(), "Annotation should be a TextAnnotation.");

                                var textAnn = (TextAnnotation)ann;

                                // Verify the rectangle dimensions and position
                                // Aspose.Pdf.Rectangle uses lower‑left (LLX, LLY) and upper‑right (URX, URY) coordinates
                                NUnit.Framework.Assert.AreEqual(ExpectedRect.Width, textAnn.Rect.Width, "Annotation width mismatch.");
                                NUnit.Framework.Assert.AreEqual(ExpectedRect.Height, textAnn.Rect.Height, "Annotation height mismatch.");
                                NUnit.Framework.Assert.AreEqual(ExpectedRect.LLX, textAnn.Rect.LLX, "Annotation lower‑left X mismatch.");
                                NUnit.Framework.Assert.AreEqual(ExpectedRect.LLY, textAnn.Rect.LLY, "Annotation lower‑left Y mismatch.");
                            }
                        }
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    public class Program
    {
        public static void Main() { /* No‑op – tests are executed by the test runner */ }
    }
}
