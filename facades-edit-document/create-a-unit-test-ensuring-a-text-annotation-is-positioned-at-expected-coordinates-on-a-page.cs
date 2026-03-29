using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using NUnit.Framework;

namespace AsposePdfTests
{
    [TestFixture]
    public class TextAnnotationPositionTests
    {
        private const string OutputPath = "test_output.pdf";
        private const double ExpectedLlx = 100.0;
        private const double ExpectedLly = 500.0;
        private const double ExpectedUrx = 200.0;
        private const double ExpectedUry = 550.0;
        private const double Tolerance = 0.001;

        [Test]
        public void TextAnnotation_IsAtExpectedPosition()
        {
            // Create a PDF and add a TextAnnotation at the expected rectangle
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(ExpectedLlx, ExpectedLly, ExpectedUrx, ExpectedUry);
                TextAnnotation annotation = new TextAnnotation(page, rect);
                annotation.Contents = "Sample note";
                page.Annotations.Add(annotation);
                doc.Save(OutputPath);
            }

            // Load the PDF and verify the annotation's rectangle matches the expected coordinates
            using (Document loadedDoc = new Document(OutputPath))
            {
                Page loadedPage = loadedDoc.Pages[1]; // one‑based indexing
                Annotation loadedAnnotation = loadedPage.Annotations[1]; // first annotation
                TextAnnotation loadedTextAnnotation = loadedAnnotation as TextAnnotation;
                Assert.IsNotNull(loadedTextAnnotation, "Loaded annotation should be a TextAnnotation.");

                Aspose.Pdf.Rectangle loadedRect = loadedTextAnnotation.Rect;
                Assert.AreEqual(ExpectedLlx, loadedRect.LLX, Tolerance, "LLX does not match expected value.");
                Assert.AreEqual(ExpectedLly, loadedRect.LLY, Tolerance, "LLY does not match expected value.");
                Assert.AreEqual(ExpectedUrx, loadedRect.URX, Tolerance, "URX does not match expected value.");
                Assert.AreEqual(ExpectedUry, loadedRect.URY, Tolerance, "URY does not match expected value.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main() { /* No‑op */ }
    }
}

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the real NUnit package is not referenced.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Simple equality check for generic types.
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        // Double overload that respects a tolerance value.
        public static void AreEqual(double expected, double actual, double tolerance, string message = null)
        {
            if (double.IsNaN(expected) || double.IsNaN(actual) || Math.Abs(expected - actual) > tolerance)
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}> +/- {tolerance}. Actual:<{actual}>.");
        }

        // Null‑check helper.
        public static void IsNotNull(object obj, string message = null)
        {
            if (obj == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }
    }
}