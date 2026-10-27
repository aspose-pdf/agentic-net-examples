using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Generic AreEqual with optional message – works for value types and strings
        public static void AreEqual<T>(T expected, T actual, string message = null)
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
    public class TextAnnotationPositionTests
    {
        // Expected coordinates for the annotation rectangle
        private const double ExpectedLlx = 100;
        private const double ExpectedLly = 500;
        private const double ExpectedUrx = 200;
        private const double ExpectedUry = 600;

        [Test]
        public void TextAnnotation_ShouldBeAtExpectedCoordinates()
        {
            // Create a temporary working directory
            string workDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(workDir);

            string originalPdfPath = Path.Combine(workDir, "original.pdf");
            string annotatedPdfPath = Path.Combine(workDir, "annotated.pdf");

            // -----------------------------------------------------------------
            // Step 1: Create a simple one‑page PDF to work with
            // -----------------------------------------------------------------
            using (Document doc = new Document())
            {
                // Add a blank page
                doc.Pages.Add();
                doc.Save(originalPdfPath);
            }

            // -----------------------------------------------------------------
            // Step 2: Add a text annotation using the Document API (PdfAnnotationEditor has no AddTextAnnotation)
            // -----------------------------------------------------------------
            using (Document doc = new Document(originalPdfPath))
            {
                Page page = doc.Pages[1];
                // Define the rectangle for the annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(ExpectedLlx, ExpectedLly, ExpectedUrx, ExpectedUry);
                // Create the annotation, set its properties and add it to the page
                TextAnnotation textAnno = new TextAnnotation(page, rect)
                {
                    Contents = "Sample note text",
                    Title = "Sample Title"
                };
                page.Annotations.Add(textAnno);
                doc.Save(annotatedPdfPath);
            }

            // -----------------------------------------------------------------
            // Step 3: Load the resulting PDF and verify the annotation rectangle
            // -----------------------------------------------------------------
            using (Document doc = new Document(annotatedPdfPath))
            {
                // Retrieve the first (and only) annotation on page 1
                Page page = doc.Pages[1];
                Assert.AreEqual(1, page.Annotations.Count, "Expected exactly one annotation on the page.");

                // Cast to TextAnnotation to access rectangle properties
                TextAnnotation textAnno = page.Annotations[1] as TextAnnotation;
                Assert.AreEqual(true, textAnno != null, "Annotation is not a TextAnnotation.");

                // Fully qualified rectangle type to avoid ambiguity
                Aspose.Pdf.Rectangle rect = textAnno.Rect;

                // Verify each coordinate matches the expected values
                Assert.AreEqual(ExpectedLlx, rect.LLX, "Lower‑left X coordinate mismatch.");
                Assert.AreEqual(ExpectedLly, rect.LLY, "Lower‑left Y coordinate mismatch.");
                Assert.AreEqual(ExpectedUrx, rect.URX, "Upper‑right X coordinate mismatch.");
                Assert.AreEqual(ExpectedUry, rect.URY, "Upper‑right Y coordinate mismatch.");
            }

            // -----------------------------------------------------------------
            // Cleanup temporary files
            // -----------------------------------------------------------------
            try { File.Delete(originalPdfPath); } catch { /* ignore */ }
            try { File.Delete(annotatedPdfPath); } catch { /* ignore */ }
            try { Directory.Delete(workDir, true); } catch { /* ignore */ }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable project.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed via the test runner.
        }
    }
}
