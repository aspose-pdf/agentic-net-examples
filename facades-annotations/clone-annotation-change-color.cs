using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Clones an annotation from a source page, changes its color, and adds the clone to a target page.
    /// </summary>
    /// <param name="pdfPath">Path to the input PDF file.</param>
    /// <param name="sourcePage">1‑based index of the page containing the original annotation.</param>
    /// <param name="annotationIndex">1‑based index of the annotation on the source page to clone.</param>
    /// <param name="targetPage">1‑based index of the page where the cloned annotation will be placed.</param>
    /// <param name="newColor">The color to apply to the cloned annotation.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    public static void CloneAnnotationChangeColor(
        string pdfPath,
        int sourcePage,
        int annotationIndex,
        int targetPage,
        Aspose.Pdf.Color newColor,
        string outputPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"Input PDF not found: {pdfPath}");

        // Bind the PDF document to the PdfAnnotationEditor facade.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Extract annotations from the source page.
            // Resolve overload ambiguity by explicitly casting null to string[] (all types).
            IList<Annotation> sourceAnnotations = editor.ExtractAnnotations(sourcePage, sourcePage, (string[])null);

            if (sourceAnnotations == null || sourceAnnotations.Count < annotationIndex)
                throw new ArgumentOutOfRangeException(nameof(annotationIndex), "Annotation index is out of range.");

            // Get the specific annotation to clone.
            Annotation original = sourceAnnotations[annotationIndex - 1];

            // For demonstration we handle TextAnnotation (markup) cloning.
            // Other annotation types can be handled similarly by checking original.GetType().
            if (original is TextAnnotation textAnnot)
            {
                // Create a new TextAnnotation on the target page with the same rectangle.
                Aspose.Pdf.Rectangle rect = textAnnot.Rect;
                TextAnnotation clone = new TextAnnotation(editor.Document.Pages[targetPage], rect)
                {
                    // Copy common properties.
                    Title    = textAnnot.Title,
                    Contents = textAnnot.Contents,
                    Subject  = textAnnot.Subject,
                    // Apply the new color.
                    Color    = newColor,
                    // Preserve other visual settings if needed.
                    Open     = textAnnot.Open,
                    Modified = DateTime.Now
                };

                // Add the cloned annotation to the target page.
                editor.Document.Pages[targetPage].Annotations.Add(clone);
            }
            else
            {
                // If the annotation type is not handled, throw an informative exception.
                throw new NotSupportedException($"Cloning of annotation type '{original.GetType().Name}' is not implemented.");
            }

            // Save the modified document to the specified output path.
            editor.Save(outputPath);
        }
    }
}

// Dummy entry point to satisfy the compiler when building an executable project.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the library methods can be invoked from other code or unit tests.
    }
}

// Example usage (commented out – not executed in Main):
// PdfAnnotationHelper.CloneAnnotationChangeColor(
//     pdfPath: "input.pdf",
//     sourcePage: 1,
//     annotationIndex: 1,
//     targetPage: 2,
//     newColor: Aspose.Pdf.Color.Red,
//     outputPath: "output.pdf");