using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

public static class AnnotationHelper
{
    /// <summary>
    /// Clones an existing annotation on a page, modifies some of its properties,
    /// and adds the cloned annotation back to the same page.
    /// Uses Aspose.Pdf.Facades for loading and saving the PDF.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    /// <param name="pageNumber">1‑based page number containing the annotation.</param>
    /// <param name="annotationIndex">1‑based index of the annotation to clone.</param>
    public static void CloneModifyAddAnnotation(string inputPdf, string outputPdf, int pageNumber, int annotationIndex)
    {
        // Bind the PDF using the Facade (load rule)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Access the underlying Document (core API)
            Document doc = editor.Document;

            // Retrieve the target page (1‑based indexing rule)
            Page page = doc.Pages[pageNumber];

            // Get the original annotation (1‑based indexing rule)
            Annotation original = page.Annotations[annotationIndex];

            // Prepare a variable for the cloned annotation
            Annotation cloned = null;

            // Clone based on the concrete annotation type.
            // TextAnnotation example (has Open property)
            if (original is TextAnnotation textOrig)
            {
                cloned = new TextAnnotation(page, textOrig.Rect)
                {
                    Title    = textOrig.Title + " (Clone)",
                    Contents = textOrig.Contents + " (modified)",
                    Color    = Aspose.Pdf.Color.Red,
                    Modified = DateTime.Now,
                    Subject  = textOrig.Subject,
                    Open     = textOrig.Open // TextAnnotation supports Open
                };
            }
            // StampAnnotation example (does NOT have Open property)
            else if (original is StampAnnotation stampOrig)
            {
                cloned = new StampAnnotation(page, stampOrig.Rect)
                {
                    Title    = stampOrig.Title + " (Clone)",
                    Contents = stampOrig.Contents + " (modified)",
                    Color    = Aspose.Pdf.Color.Blue,
                    Modified = DateTime.Now,
                    Subject  = stampOrig.Subject,
                    // Open property removed – not supported by StampAnnotation
                    Icon     = stampOrig.Icon
                };
            }
            // Add more annotation types as needed...

            // If a clone was created, add it back to the page's annotation collection
            if (cloned != null)
            {
                page.Annotations.Add(cloned);
            }

            // Save the modified document (save rule)
            editor.Save(outputPdf);
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (can be removed or replaced in real scenarios)
        // AnnotationHelper.CloneModifyAddAnnotation("input.pdf", "output.pdf", 1, 1);
    }
}
