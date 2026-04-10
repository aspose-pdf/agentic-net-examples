using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsInstanceOf<T>(object obj, string message = null)
        {
            if (!(obj is T))
                throw new Exception(message ?? $"Assert.IsInstanceOf failed. Object is not of type {typeof(T)}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfAnnotationEditorTests
    {
        private const string TempFolder = "TempTestFiles";

        [SetUp]
        public void Setup()
        {
            // Ensure a clean temporary folder for each test run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [Test]
        public void ModifyAnnotations_ShouldUpdateSubjectProperty()
        {
            // Arrange: create a PDF with a single TextAnnotation having an initial subject
            string inputPath = Path.Combine(TempFolder, "input.pdf");
            string outputPath = Path.Combine(TempFolder, "output.pdf");

            using (Document doc = new Document())
            {
                // Add a blank page (Pages collection is 1‑based)
                doc.Pages.Add();

                // Define annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the annotation, set the original subject, and add it to the page
                TextAnnotation originalAnnot = new TextAnnotation(doc.Pages[1], rect)
                {
                    Subject = "Original Subject",
                    Title = "Tester",
                    Contents = "Test annotation"
                };
                doc.Pages[1].Annotations.Add(originalAnnot);

                // Save the PDF to disk
                doc.Save(inputPath);
            }

            // Act: use PdfAnnotationEditor to modify the Subject of the annotation
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the previously saved PDF
                editor.BindPdf(inputPath);

                // Load the same document to obtain a Page instance required for the template annotation
                using (Document tempDoc = new Document(inputPath))
                {
                    // Prepare a template annotation with the new Subject value.
                    // The constructor requires a Page and a Rectangle; a zero‑size rectangle is sufficient for a template.
                    TextAnnotation templateAnnot = new TextAnnotation(tempDoc.Pages[1], new Aspose.Pdf.Rectangle(0, 0, 0, 0))
                    {
                        Subject = "New Subject"
                    };

                    // Apply modification to page 1 (start = end = 1)
                    editor.ModifyAnnotations(1, 1, templateAnnot);
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }

            // Assert: load the modified PDF and verify the Subject was updated
            using (Document resultDoc = new Document(outputPath))
            {
                // Retrieve the first annotation on the first page (annotations are 1‑based)
                Annotation retrieved = resultDoc.Pages[1].Annotations[1];
                Assert.IsInstanceOf<TextAnnotation>(retrieved, "Annotation should be a TextAnnotation.");

                TextAnnotation modifiedAnnot = (TextAnnotation)retrieved;
                Assert.AreEqual("New Subject", modifiedAnnot.Subject, "Subject property was not updated correctly.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static class Program
    {
        public static void Main() { }
    }
}
