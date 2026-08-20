using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsposePdfTests
{
    [TestClass]
    public class PdfAnnotationEditorTests
    {
        private const string OriginalSubject = "Original Subject";
        private const string UpdatedSubject = "Updated Subject";

        // Helper to create a PDF with a single TextAnnotation on the first page
        private static string CreatePdfWithAnnotation()
        {
            // Create a temporary file path for the PDF
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Create a new PDF document and add a page
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Define the annotation rectangle (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation with an initial subject
                TextAnnotation textAnnot = new TextAnnotation(page, rect)
                {
                    Subject = OriginalSubject,
                    Title = "Test Author",
                    Contents = "Test contents",
                    Color = Aspose.Pdf.Color.Yellow,
                    Open = true
                };

                // Add the annotation to the page
                page.Annotations.Add(textAnnot);

                // Save the PDF to the temporary path
                doc.Save(tempPdfPath);
            }

            return tempPdfPath;
        }

        // Helper to read the subject of the first annotation on the first page
        private static string GetFirstAnnotationSubject(string pdfPath)
        {
            using (Document doc = new Document(pdfPath))
            {
                // Pages are 1‑based
                Page page = doc.Pages[1];
                if (page.Annotations.Count == 0)
                    throw new InvalidOperationException("No annotations found on the page.");

                // Annotations collection is also 1‑based
                Annotation annot = page.Annotations[1];
                // Cast to TextAnnotation to access the Subject property
                if (annot is TextAnnotation textAnnot)
                    return textAnnot.Subject;

                throw new InvalidOperationException("Annotation is not a TextAnnotation.");
            }
        }

        [TestMethod]
        public void ModifyAnnotations_ShouldUpdateSubjectProperty()
        {
            // Step 1: Create a PDF with a known annotation subject
            string sourcePdf = CreatePdfWithAnnotation();

            // Step 2: Prepare a temporary file for the modified output
            string modifiedPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Step 3: Use PdfAnnotationEditor to modify the Subject property
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF
                editor.BindPdf(sourcePdf);

                // Create a dummy TextAnnotation that only carries the new Subject value.
                // The constructor requires a Page and a Rectangle, so we obtain a page from the source PDF.
                using (Document tempDoc = new Document(sourcePdf))
                {
                    Page firstPage = tempDoc.Pages[1];
                    Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                    TextAnnotation newValues = new TextAnnotation(firstPage, dummyRect)
                    {
                        Subject = UpdatedSubject
                    };

                    // Apply the modification to pages 1 through 1 (inclusive)
                    editor.ModifyAnnotations(1, 1, newValues);
                }

                // Save the modified document
                editor.Save(modifiedPdf);
            }

            // Step 4: Verify that the Subject property was updated
            string resultingSubject = GetFirstAnnotationSubject(modifiedPdf);
            Assert.AreEqual(UpdatedSubject, resultingSubject, "The annotation subject was not updated as expected.");

            // Cleanup temporary files
            File.Delete(sourcePdf);
            File.Delete(modifiedPdf);
        }
    }
}

// Minimal MSTest stubs – added to allow the test code to compile without a
// reference to the full Microsoft.VisualStudio.TestTools.UnitTesting package.
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an
// executable. In a real test project this class would not be required because
// the project type would be a library.
public class Program
{
    public static void Main() { }
}