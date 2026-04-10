using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added to bring stub attributes into scope

// Minimal NUnit stubs to allow compilation without the real NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null!)
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
    public void DeleteAnnotations_RemovesAllAnnotations()
    {
        // Create a PDF in memory with two text annotations.
        using (MemoryStream originalStream = new MemoryStream())
        {
            using (Document doc = new Document())
            {
                // Add a single page.
                Page page = doc.Pages.Add();

                // First annotation.
                Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
                TextAnnotation ann1 = new TextAnnotation(page, rect1)
                {
                    Title = "Note1",
                    Contents = "First annotation"
                };
                page.Annotations.Add(ann1);

                // Second annotation.
                Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 400, 200, 450);
                TextAnnotation ann2 = new TextAnnotation(page, rect2)
                {
                    Title = "Note2",
                    Contents = "Second annotation"
                };
                page.Annotations.Add(ann2);

                // Save the PDF to the memory stream.
                doc.Save(originalStream);
            }

            // Verify the PDF initially contains two annotations.
            originalStream.Position = 0;
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(originalStream);
                int beforeCount = editor.Document.Pages[1].Annotations.Count;
                Assert.AreEqual(2, beforeCount, "Initial annotation count should be 2.");

                // Delete all annotations using the DeleteAnnotations method.
                editor.DeleteAnnotations();

                // Save the modified PDF to another memory stream.
                using (MemoryStream modifiedStream = new MemoryStream())
                {
                    editor.Save(modifiedStream);
                    modifiedStream.Position = 0;

                    // Load the result and confirm that no annotations remain.
                    using (Document resultDoc = new Document(modifiedStream))
                    {
                        int afterCount = resultDoc.Pages[1].Annotations.Count;
                        Assert.AreEqual(0, afterCount, "All annotations should be removed.");
                    }
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as a console application.
public static class Program
{
    public static void Main() => Console.WriteLine("Test assembly loaded.");
}