using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeTearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AnnotationDeletionTests
    {
        private const string TempFolder = "TempTestFiles";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure a clean temporary folder for test files
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Remove temporary files after all tests run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [Test]
        public void DeleteAnnotations_RemovesAllAnnotationsFromPage()
        {
            // Arrange: create a PDF with three text annotations on page 1
            string pdfPath = Path.Combine(TempFolder, "sample.pdf");
            using (Document doc = new Document())
            {
                // Add a single page
                Page page = doc.Pages.Add();

                // Helper to create a TextAnnotation
                TextAnnotation CreateAnnotation(string title, string contents, double llx, double lly, double urx, double ury)
                {
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                    TextAnnotation annot = new TextAnnotation(page, rect)
                    {
                        Title = title,
                        Contents = contents,
                        Open = true,
                        Icon = TextIcon.Note
                    };
                    return annot;
                }

                // Add three annotations
                page.Annotations.Add(CreateAnnotation("Note1", "First note", 100, 700, 200, 750));
                page.Annotations.Add(CreateAnnotation("Note2", "Second note", 100, 600, 200, 650));
                page.Annotations.Add(CreateAnnotation("Note3", "Third note", 100, 500, 200, 550));

                // Verify that annotations were added
                Assert.AreEqual(3, page.Annotations.Count, "Setup failed: expected 3 annotations before deletion.");

                // Save the PDF to disk
                doc.Save(pdfPath);
            }

            // Act: delete all annotations on page 1 using PdfAnnotationEditor
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfPath);
            editor.DeleteAnnotations("1"); // page numbers are 1‑based and method expects a string
            editor.Save(pdfPath); // persist changes

            // Assert: reload the document and ensure no annotations remain on page 1
            using (Document resultDoc = new Document(pdfPath))
            {
                Page resultPage = resultDoc.Pages[1];
                Assert.AreEqual(0, resultPage.Annotations.Count, "All annotations should have been removed from page 1.");
            }

            // Cleanup: delete the temporary PDF file
            if (File.Exists(pdfPath))
                File.Delete(pdfPath);
        }
    }

    // Dummy entry point to satisfy the console‑application project type.
    public static class Program
    {
        public static void Main() { }
    }
}
