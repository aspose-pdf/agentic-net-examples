using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // <-- added

// Minimal NUnit stubs to allow compilation without the real NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Equality without tolerance
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        // Equality with tolerance for numeric types (float, double, decimal)
        public static void AreEqual(double expected, double actual, double tolerance, string? message = null)
        {
            if (double.IsNaN(expected) || double.IsNaN(actual) || Math.Abs(expected - actual) > tolerance)
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>. Tolerance:<{tolerance}>.");
        }

        // Overload for float (calls double version)
        public static void AreEqual(float expected, float actual, float tolerance, string? message = null)
        {
            AreEqual((double)expected, (double)actual, (double)tolerance, message);
        }

        public static void IsNotNull(object? anObject, string? message = null)
        {
            if (anObject == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }
    }
}

namespace AsposePdfFacadesTests
{
    [TestFixture]
    public class AddTextCoordinateTests
    {
        // Expected coordinates for the free‑text annotation
        private const float ExpectedLowerLeftX = 100f;
        private const float ExpectedLowerLeftY = 200f;
        private const float ExpectedUpperRightX = 300f;
        private const float ExpectedUpperRightY = 250f;

        [Test]
        public void AddedText_ShouldAppearAtExpectedCoordinates()
        {
            // Temporary files for the source and result PDFs
            string sourcePdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
            string resultPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");

            try
            {
                // ------------------------------------------------------------
                // 1. Create a blank PDF with a single page and save it.
                // ------------------------------------------------------------
                using (Document doc = new Document())
                {
                    // Add an empty page (Aspose.Pdf uses 1‑based indexing)
                    doc.Pages.Add();
                    doc.Save(sourcePdfPath);
                }

                // ------------------------------------------------------------
                // 2. Use PdfContentEditor (a Facade) to add free‑text at the
                //    desired rectangle. The rectangle is defined with System.Drawing.Rectangle.
                // ------------------------------------------------------------
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the previously created PDF
                    editor.BindPdf(sourcePdfPath);

                    // Calculate width and height for the System.Drawing.Rectangle
                    int rectWidth = (int)(ExpectedUpperRightX - ExpectedLowerLeftX);
                    int rectHeight = (int)(ExpectedUpperRightY - ExpectedLowerLeftY);

                    // Create the rectangle at the expected lower‑left corner (fully qualified)
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                        (int)ExpectedLowerLeftX,
                        (int)ExpectedLowerLeftY,
                        rectWidth,
                        rectHeight);

                    // Add the free‑text annotation on page 1
                    editor.CreateFreeText(rect, "Sample Text", 1);

                    // Save the modified PDF
                    editor.Save(resultPdfPath);
                }

                // ------------------------------------------------------------
                // 3. Load the resulting PDF and verify the annotation rectangle.
                // ------------------------------------------------------------
                using (Document resultDoc = new Document(resultPdfPath))
                {
                    // Retrieve the first annotation on page 1
                    Page page = resultDoc.Pages[1];
                    Assert.AreEqual(1, page.Annotations.Count, "Expected exactly one annotation.");

                    // The annotation created by CreateFreeText is a FreeTextAnnotation
                    var freeText = page.Annotations[1] as Aspose.Pdf.Annotations.FreeTextAnnotation;
                    Assert.IsNotNull(freeText, "Annotation is not a FreeTextAnnotation.");

                    // Verify the rectangle coordinates (property is Rect, not Rectangle)
                    Aspose.Pdf.Rectangle actualRect = freeText.Rect;

                    // Aspose.Pdf.Rectangle uses lower‑left (llx,lly) and upper‑right (urx,ury)
                    Assert.AreEqual(ExpectedLowerLeftX, actualRect.LLX, 0.01f, "Lower‑left X does not match.");
                    Assert.AreEqual(ExpectedLowerLeftY, actualRect.LLY, 0.01f, "Lower‑left Y does not match.");
                    Assert.AreEqual(ExpectedUpperRightX, actualRect.URX, 0.01f, "Upper‑right X does not match.");
                    Assert.AreEqual(ExpectedUpperRightY, actualRect.URY, 0.01f, "Upper‑right Y does not match.");
                }
            }
            finally
            {
                // Clean up temporary files
                if (File.Exists(sourcePdfPath))
                    File.Delete(sourcePdfPath);
                if (File.Exists(resultPdfPath))
                    File.Delete(resultPdfPath);
            }
        }
    }

    // Dummy entry point so the project compiles as a console application.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed via the test runner.
        }
    }
}
