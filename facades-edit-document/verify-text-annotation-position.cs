using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the real NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsNotNull(object obj, string message = null)
        {
            if (obj == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }
    }
}

namespace AsposePdfTests
{
    using NUnit.Framework;

    [TestFixture]
    public class TextAnnotationPositionTests
    {
        [Test]
        public void TextAnnotation_IsPlacedAtExpectedCoordinates()
        {
            // Expected rectangle coordinates (lower‑left origin)
            const double expectedLlx = 100;
            const double expectedLly = 200;
            const double expectedUrx = 150; // llx + width (50)
            const double expectedUry = 230; // lly + height (30)

            // 1. Create a simple PDF with a single blank page.
            using (Document sourceDoc = new Document())
            {
                sourceDoc.Pages.Add(); // adds first page (1‑based indexing)

                // 2. Save the source PDF into a memory stream.
                using (MemoryStream sourceStream = new MemoryStream())
                {
                    sourceDoc.Save(sourceStream);
                    sourceStream.Position = 0; // reset for reading

                    // 3. Use PdfContentEditor (Facade) to add a text annotation.
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(sourceStream);

                    // Rectangle uses System.Drawing.Rectangle (X,Y = lower‑left in Aspose conversion)
                    System.Drawing.Rectangle annotationRect = new System.Drawing.Rectangle((int)expectedLlx, (int)expectedLly, 50, 30);
                    editor.CreateText(
                        annotationRect,
                        "TestTitle",          // title of the annotation
                        "TestContent",        // contents displayed in the popup
                        true,                 // open flag
                        "Note",               // icon name
                        1                     // page number (1‑based)
                    );

                    // 4. Save the modified PDF into another memory stream.
                    using (MemoryStream resultStream = new MemoryStream())
                    {
                        editor.Save(resultStream);
                        resultStream.Position = 0; // reset for reading

                        // 5. Load the resulting PDF and retrieve the annotation.
                        using (Document resultDoc = new Document(resultStream))
                        {
                            // Annotations collection is also 1‑based.
                            Annotation rawAnnotation = resultDoc.Pages[1].Annotations[1];
                            Assert.IsNotNull(rawAnnotation, "Annotation should exist on the page.");

                            // Cast to TextAnnotation to access rectangle.
                            var textAnnotation = rawAnnotation as TextAnnotation;
                            Assert.IsNotNull(textAnnotation, "Annotation should be a TextAnnotation.");

                            // Verify the rectangle coordinates.
                            Aspose.Pdf.Rectangle actualRect = textAnnotation.Rect;
                            Assert.AreEqual(expectedLlx, actualRect.LLX, "LLX does not match.");
                            Assert.AreEqual(expectedLly, actualRect.LLY, "LLY does not match.");
                            Assert.AreEqual(expectedUrx, actualRect.URX, "URX does not match.");
                            Assert.AreEqual(expectedUry, actualRect.URY, "URY does not match.");
                        }
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed via the test runner.
        }
    }
}
