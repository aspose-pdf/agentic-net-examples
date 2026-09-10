using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class AnnotationCloner
{
    /// <summary>
    /// Clones an annotation from a source page, changes its color, and adds the clone to a target page.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF file.</param>
    /// <param name="sourcePage">1‑based index of the page containing the annotation to clone.</param>
    /// <param name="annotationIndex">1‑based index of the annotation on the source page.</param>
    /// <param name="targetPage">1‑based index of the page where the cloned annotation will be placed.</param>
    /// <param name="newColor">The new color to apply to the cloned annotation.</param>
    /// <param name="outputPdf">Path where the resulting PDF will be saved.</param>
    public static void CloneAnnotation(
        string inputPdf,
        int sourcePage,
        int annotationIndex,
        int targetPage,
        Aspose.Pdf.Color newColor,
        string outputPdf)
    {
        // Load the PDF document (use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the source annotation (pages and annotation collections are 1‑based)
            Annotation srcAnnotation = doc.Pages[sourcePage].Annotations[annotationIndex];

            // Prepare a clone based on the concrete type of the source annotation.
            // Only a few common types are handled explicitly; others fall back to a TextAnnotation.
            Annotation clonedAnnotation;

            if (srcAnnotation is TextAnnotation textAnn)
            {
                // Clone a TextAnnotation
                clonedAnnotation = new TextAnnotation(doc.Pages[targetPage], textAnn.Rect)
                {
                    Contents = textAnn.Contents,
                    Color = newColor,
                    Title = textAnn.Title,
                    Subject = textAnn.Subject,
                    // The 'Open' property exists on TextAnnotation, keep it if present.
                    Open = textAnn.Open
                };
            }
            else if (srcAnnotation is StampAnnotation stampAnn)
            {
                // Clone a StampAnnotation (StampAnnotation does NOT have an 'Open' property)
                clonedAnnotation = new StampAnnotation(doc.Pages[targetPage], stampAnn.Rect)
                {
                    Contents = stampAnn.Contents,
                    Color = newColor,
                    Icon = stampAnn.Icon,
                    Title = stampAnn.Title,
                    Subject = stampAnn.Subject
                };
            }
            else
            {
                // Generic fallback – use TextAnnotation as a concrete type.
                clonedAnnotation = new TextAnnotation(doc.Pages[targetPage], srcAnnotation.Rect)
                {
                    Contents = srcAnnotation.Contents,
                    Color = newColor
                };
            }

            // Add the cloned annotation to the target page
            doc.Pages[targetPage].Annotations.Add(clonedAnnotation);

            // Save the modified document. Using Document.Save is sufficient; no need for PdfAnnotationEditor.
            doc.Save(outputPdf);
        }
    }
}

// Dummy entry point to satisfy the compiler when building an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (can be removed or replaced in real scenarios)
        // AnnotationCloner.CloneAnnotation("input.pdf", 1, 1, 2, Aspose.Pdf.Color.Red, "output.pdf");
    }
}
