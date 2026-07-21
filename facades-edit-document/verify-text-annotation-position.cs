using System;
using System.Drawing; // System.Drawing.Rectangle for PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework; // <-- added to bring stub attributes into scope

// Minimal NUnit stubs – add when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Simple equality check without tolerance.
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        // Equality check with tolerance for floating‑point values.
        public static void AreEqual(double expected, double actual, double delta, string? message = null)
        {
            if (Math.Abs(expected - actual) > delta)
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>. Tolerance:<{delta}>.");
        }

        // Generic version that forwards to the double overload (used for float values).
        public static void AreEqual(float expected, float actual, double delta, string? message = null)
        {
            AreEqual((double)expected, (double)actual, delta, message);
        }

        // Type‑checking helper.
        public static void IsInstanceOf<T>(object obj, string? message = null)
        {
            if (!(obj is T))
                throw new Exception(message ?? $"Assert.IsInstanceOf failed. Expected type:<{typeof(T)}>. Actual type:<{obj?.GetType()}>.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class TextAnnotationPositionTests
    {
        [Test]
        public void TextAnnotation_ShouldBeAtExpectedCoordinates()
        {
            // Expected rectangle (lower‑left X,Y and size) in points
            const float expectedX = 100f;
            const float expectedY = 200f;
            const float expectedWidth = 50f;
            const float expectedHeight = 50f;

            // Create a new PDF document with a single blank page
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // one page added (index 1)

                // Use PdfContentEditor (Facade) to add a Text (sticky‑note) annotation
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc); // bind the in‑memory document

                // System.Drawing.Rectangle is required by CreateText
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                    (int)expectedX,
                    (int)expectedY,
                    (int)expectedWidth,
                    (int)expectedHeight);

                // Add the annotation on page 1
                editor.CreateText(
                    rect,
                    title: "Note Title",
                    contents: "Sample annotation",
                    open: true,
                    icon: "Note",   // valid icon name
                    page: 1);

                // Retrieve the annotation that was just added
                // Annotations collection is 1‑based in Aspose.Pdf
                Annotation ann = doc.Pages[1].Annotations[1];
                Assert.IsInstanceOf<TextAnnotation>(ann, "Annotation should be a TextAnnotation.");

                var textAnn = (TextAnnotation)ann;

                // The annotation rectangle is returned as Aspose.Pdf.Rectangle
                Aspose.Pdf.Rectangle actualRect = textAnn.Rect;

                // Verify lower‑left coordinates
                Assert.AreEqual(expectedX, actualRect.LLX, 0.01, "Lower‑left X coordinate mismatch.");
                Assert.AreEqual(expectedY, actualRect.LLY, 0.01, "Lower‑left Y coordinate mismatch.");

                // Verify upper‑right coordinates (computed from width/height)
                Assert.AreEqual(expectedX + expectedWidth, actualRect.URX, 0.01, "Upper‑right X coordinate mismatch.");
                Assert.AreEqual(expectedY + expectedHeight, actualRect.URY, 0.01, "Upper‑right Y coordinate mismatch.");
            }
        }
    }

    // Dummy entry point to satisfy the console‑app requirement when the project is built as an executable.
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed via the NUnit runner.
        }
    }
}
