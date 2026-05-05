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
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

[TestFixture]
public class PdfAnnotationEditorTests
{
    [Test]
    public void ModifyAnnotations_UpdatesSubject()
    {
        // Create a PDF with a single page and a TextAnnotation
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define rectangle for the annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a TextAnnotation with an initial Subject value
            TextAnnotation originalAnnot = new TextAnnotation(page, rect)
            {
                Subject = "Original Subject",
                Contents = "Sample note",
                Title = "Author"
            };
            page.Annotations.Add(originalAnnot);

            // Save the original PDF to a memory stream
            using (MemoryStream originalStream = new MemoryStream())
            {
                doc.Save(originalStream);
                originalStream.Position = 0;

                // Bind the PDF to PdfAnnotationEditor
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(originalStream);

                // Prepare an annotation containing the new Subject value.
                // Use the same page/rectangle constructor required by TextAnnotation.
                TextAnnotation modifyAnnot = new TextAnnotation(page, rect)
                {
                    Subject = "Updated Subject"
                };

                // Apply the modification to page 1, annotation index 1
                editor.ModifyAnnotations(1, 1, modifyAnnot);

                // Save the modified PDF to another memory stream
                using (MemoryStream modifiedStream = new MemoryStream())
                {
                    editor.Save(modifiedStream);
                    modifiedStream.Position = 0;

                    // Load the modified PDF and verify the Subject was updated
                    using (Document resultDoc = new Document(modifiedStream))
                    {
                        var resultPage = resultDoc.Pages[1];
                        var resultAnnot = (TextAnnotation)resultPage.Annotations[1];
                        Assert.AreEqual("Updated Subject", resultAnnot.Subject);
                    }
                }

                // Clean up the editor
                editor.Close();
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static void Main(string[] args) { }
}
