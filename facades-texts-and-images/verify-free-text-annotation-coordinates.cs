using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs – only the members used by the tests are provided.
// If the real NUnit package is referenced these definitions are ignored by the compiler.
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

        public static void IsInstanceOf<T>(object obj, string? message = null)
        {
            if (!(obj is T))
            {
                throw new Exception(message ?? $"Assert.IsInstanceOf failed. Expected type:<{typeof(T)}> but was:<{obj?.GetType()}>.");
            }
        }
    }
}

namespace AsposePdfFacadesTests
{
    [TestFixture]
    public class PdfContentEditorTests
    {
        // Helper to create a simple one‑page PDF in memory
        private static Document CreateBlankPdf()
        {
            // Document constructor creates an empty PDF; add a page to have something to annotate
            Document doc = new Document();
            doc.Pages.Add();
            return doc;
        }

        [Test]
        public void AddTextCoordinates_ShouldPlaceFreeTextAtExpectedLocation()
        {
            // Expected rectangle coordinates (points). 
            // System.Drawing.Rectangle uses (x, y, width, height) where (x, y) is the upper‑left corner.
            // Aspose.Pdf expects a rectangle defined by lower‑left (llx,lly) and upper‑right (urx,ury).
            // For the purpose of this test we compare the values after conversion.
            int rectX = 100;      // lower‑left X
            int rectY = 200;      // lower‑left Y
            int rectWidth = 300;  // width
            int rectHeight = 50;  // height

            // System.Drawing.Rectangle for CreateFreeText (origin is top‑left, so we keep the same values)
            System.Drawing.Rectangle drawingRect = new System.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);
            const string annotationText = "UnitTest Text";

            // Paths for temporary files
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            string resultPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // -----------------------------------------------------------------
                // 1. Create a blank PDF and save it (source for editing)
                // -----------------------------------------------------------------
                using (Document sourceDoc = CreateBlankPdf())
                {
                    sourceDoc.Save(tempPdfPath);
                }

                // -----------------------------------------------------------------
                // 2. Use PdfContentEditor (Facade) to add free‑text annotation
                // -----------------------------------------------------------------
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind to the source PDF file
                    editor.BindPdf(tempPdfPath);

                    // Create free‑text annotation on page 1 at the specified rectangle
                    editor.CreateFreeText(drawingRect, annotationText, 1);

                    // Save the modified PDF
                    editor.Save(resultPdfPath);
                }

                // -----------------------------------------------------------------
                // 3. Load the resulting PDF and verify the annotation's rectangle
                // -----------------------------------------------------------------
                using (Document resultDoc = new Document(resultPdfPath))
                {
                    // Ensure the annotation exists
                    Assert.AreEqual(1, resultDoc.Pages[1].Annotations.Count, "Expected one annotation on the page.");

                    // The annotation created by CreateFreeText is a FreeTextAnnotation
                    Annotation ann = resultDoc.Pages[1].Annotations[1];
                    Assert.IsInstanceOf<FreeTextAnnotation>(ann, "Annotation should be a FreeTextAnnotation.");

                    FreeTextAnnotation freeText = (FreeTextAnnotation)ann;

                    // The rectangle returned by Aspose.Pdf is an Aspose.Pdf.Rectangle
                    Aspose.Pdf.Rectangle pdfRect = freeText.Rect;

                    // Verify lower‑left coordinates
                    Assert.AreEqual(rectX, (int)pdfRect.LLX, "Lower‑left X coordinate mismatch.");
                    Assert.AreEqual(rectY, (int)pdfRect.LLY, "Lower‑left Y coordinate mismatch.");

                    // Verify upper‑right coordinates (computed from width/height)
                    int expectedURX = rectX + rectWidth;
                    int expectedURY = rectY + rectHeight;
                    Assert.AreEqual(expectedURX, (int)pdfRect.URX, "Upper‑right X coordinate mismatch.");
                    Assert.AreEqual(expectedURY, (int)pdfRect.URY, "Upper‑right Y coordinate mismatch.");

                    // Optional: verify the annotation's contents
                    Assert.AreEqual(annotationText, freeText.Contents, "Annotation text mismatch.");
                }
            }
            finally
            {
                // Clean up temporary files
                if (File.Exists(tempPdfPath)) File.Delete(tempPdfPath);
                if (File.Exists(resultPdfPath)) File.Delete(resultPdfPath);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed via the test runner.
        }
    }
}
