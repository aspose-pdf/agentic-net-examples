using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs – used only when the real NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

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
                throw new Exception(message ?? $"Assert.IsInstanceOf failed. Expected type:<{typeof(T)}> but was:<{obj?.GetType()}>.");
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
            if (!Directory.Exists(TempFolder))
                Directory.CreateDirectory(TempFolder);
        }

        [TearDown]
        public void Cleanup()
        {
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [Test]
        public void ModifyAnnotations_ShouldUpdateSubjectProperty()
        {
            // Arrange: create a PDF with a single TextAnnotation having an initial subject.
            string inputPath = Path.Combine(TempFolder, "input.pdf");
            string outputPath = Path.Combine(TempFolder, "output.pdf");

            using (Document doc = new Document())
            {
                // Add a blank page.
                Page page = doc.Pages.Add();

                // Define annotation rectangle (fully qualified to avoid ambiguity).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the annotation with an initial subject.
                TextAnnotation txtAnnot = new TextAnnotation(page, rect)
                {
                    Subject = "Original Subject",
                    Title = "Tester",
                    Contents = "Sample annotation"
                };

                // Add the annotation to the page.
                page.Annotations.Add(txtAnnot);

                // Save the source PDF.
                doc.Save(inputPath);
            }

            // Act: modify the Subject property using PdfAnnotationEditor.ModifyAnnotations.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);

            // Prepare an annotation instance containing the new Subject value.
            // TextAnnotation does not have a parameter‑less constructor, so we create a temporary
            // document/page solely for the purpose of constructing the object.
            TextAnnotation modifyAnnot;
            using (Document dummyDoc = new Document())
            {
                Page dummyPage = dummyDoc.Pages.Add();
                Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                modifyAnnot = new TextAnnotation(dummyPage, dummyRect)
                {
                    Subject = "Updated Subject"
                };
            }

            // Apply modification to page 1 (start = end = 1).
            editor.ModifyAnnotations(1, 1, modifyAnnot);
            editor.Save(outputPath);
            editor.Close();

            // Assert: load the modified PDF and verify the Subject was updated.
            using (Document modifiedDoc = new Document(outputPath))
            {
                // Retrieve the first annotation on the first page.
                Annotation retrieved = modifiedDoc.Pages[1].Annotations[1];
                Assert.IsInstanceOf<TextAnnotation>(retrieved, "Annotation should be a TextAnnotation.");

                TextAnnotation resultAnnot = (TextAnnotation)retrieved;
                Assert.AreEqual("Updated Subject", resultAnnot.Subject, "Subject property was not updated correctly.");
            }
        }
    }

    // Adding a dummy entry point so the project compiles when built as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
