using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsNotNull(object? obj, string? message = null)
        {
            if (obj == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class TextAnnotationPositionTests
    {
        private const string OutputPdf = "text_annotation_test.pdf";

        [NUnit.Framework.Test]
        public void Annotation_Should_Be_At_Expected_Coordinates()
        {
            // Define expected rectangle coordinates (lower‑left x/y and upper‑right x/y)
            double expectedLlx = 100;
            double expectedLly = 200;
            double expectedUrx = 250;
            double expectedUry = 300;

            // Create a new PDF document with a single page
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // adds a blank page (page index 1)

                // Create the rectangle for the annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    expectedLlx, expectedLly, expectedUrx, expectedUry);

                // Create a TextAnnotation on the first page using the rectangle
                TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title = "UnitTest",
                    Contents = "Position check",
                    Open = true
                };

                // Add the annotation to the page's annotation collection
                doc.Pages[1].Annotations.Add(annotation);

                // Save the document to disk
                doc.Save(OutputPdf);
            }

            // Re‑open the saved document and verify the annotation's rectangle
            using (Document loadedDoc = new Document(OutputPdf))
            {
                // Retrieve the first annotation on the first page
                Annotation retrieved = loadedDoc.Pages[1].Annotations[1];
                NUnit.Framework.Assert.IsNotNull(retrieved, "Annotation was not found.");

                // Cast to TextAnnotation to access the Rect property
                TextAnnotation textAnn = retrieved as TextAnnotation;
                NUnit.Framework.Assert.IsNotNull(textAnn, "Retrieved annotation is not a TextAnnotation.");

                // Verify each coordinate of the rectangle
                NUnit.Framework.Assert.AreEqual(expectedLlx, textAnn.Rect.LLX, "LLX mismatch.");
                NUnit.Framework.Assert.AreEqual(expectedLly, textAnn.Rect.LLY, "LLY mismatch.");
                NUnit.Framework.Assert.AreEqual(expectedUrx, textAnn.Rect.URX, "URX mismatch.");
                NUnit.Framework.Assert.AreEqual(expectedUry, textAnn.Rect.URY, "URY mismatch.");
            }

            // Clean up the generated PDF file
            if (File.Exists(OutputPdf))
                File.Delete(OutputPdf);
        }
    }
}

// Dummy entry point to satisfy the console‑application requirement.
public static class Program
{
    public static void Main(string[] args)
    {
        // No implementation needed – unit tests are executed by the test runner.
    }
}