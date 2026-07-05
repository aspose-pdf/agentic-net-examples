using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the real NUnit package is not referenced
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

        public static void IsInstanceOf<T>(object obj, string? message = null) where T : class
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
        private const string OriginalSubject = "Original Subject";
        private const string UpdatedSubject = "Updated Subject";

        private string CreatePdfWithTextAnnotation(string filePath)
        {
            // Create a simple PDF with one page and a TextAnnotation
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                TextAnnotation textAnnot = new TextAnnotation(page, rect)
                {
                    Subject = OriginalSubject,
                    Title = "Author",
                    Contents = "Sample annotation"
                };

                page.Annotations.Add(textAnnot);
                doc.Save(filePath);
            }

            return filePath;
        }

        [Test]
        public void ModifyAnnotations_UpdatesSubject()
        {
            // Arrange: create source PDF with a known annotation
            string sourcePdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            string modifiedPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            CreatePdfWithTextAnnotation(sourcePdf);

            // Act: modify the Subject property using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(sourcePdf);

                // Prepare an annotation containing the new Subject value.
                // TextAnnotation does not have a parameter‑less constructor, so we create a temporary one.
                Document dummyDoc = new Document();
                Page dummyPage = dummyDoc.Pages.Add();
                Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                TextAnnotation newValues = new TextAnnotation(dummyPage, dummyRect)
                {
                    Subject = UpdatedSubject
                };

                // Modify annotations on page 1 (start = 1, end = 1)
                editor.ModifyAnnotations(1, 1, newValues);
                editor.Save(modifiedPdf);
            }

            // Assert: load the modified PDF and verify the Subject was updated
            using (Document doc = new Document(modifiedPdf))
            {
                Page page = doc.Pages[1];
                Assert.AreEqual(1, page.Annotations.Count, "Expected exactly one annotation on the page.");

                // Retrieve the first annotation and cast to TextAnnotation
                Annotation ann = page.Annotations[1];
                Assert.IsInstanceOf<TextAnnotation>(ann, "Annotation should be a TextAnnotation.");

                TextAnnotation textAnn = (TextAnnotation)ann;
                Assert.AreEqual(UpdatedSubject, textAnn.Subject, "Subject property was not updated correctly.");
            }

            // Cleanup temporary files
            File.Delete(sourcePdf);
            File.Delete(modifiedPdf);
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
