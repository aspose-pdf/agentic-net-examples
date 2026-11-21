using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfAnnotationCloner
{
    /// <summary>
    /// Clones an annotation from a source page, changes its color, and adds the clone to a target page.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="sourcePage">1‑based page number containing the annotation to clone.</param>
    /// <param name="annotationIndex">Zero‑based index of the annotation on the source page.</param>
    /// <param name="newColor">The color to apply to the cloned annotation.</param>
    /// <param name="targetPage">1‑based page number where the cloned annotation will be placed.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    public static void CloneAnnotationChangeColor(
        string inputPdf,
        int sourcePage,
        int annotationIndex,
        Color newColor,
        int targetPage,
        string outputPdf)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Validate page numbers.
            if (sourcePage < 1 || sourcePage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Source page {sourcePage} is out of range.");
                return;
            }
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Target page {targetPage} is out of range.");
                return;
            }

            // Get the source page's annotation collection.
            Page src = doc.Pages[sourcePage];
            if (src.Annotations == null || src.Annotations.Count == 0)
            {
                Console.Error.WriteLine($"No annotations found on page {sourcePage}.");
                return;
            }

            if (annotationIndex < 0 || annotationIndex >= src.Annotations.Count)
            {
                Console.Error.WriteLine($"Annotation index {annotationIndex} is out of range (0‑{src.Annotations.Count - 1}).");
                return;
            }

            // Clone the selected annotation.
            Annotation original = src.Annotations[annotationIndex];
            Annotation clone = (Annotation)original.Clone();

            // Change the color of the cloned annotation.
            clone.Color = newColor;

            // Add the cloned annotation to the target page.
            Page dst = doc.Pages[targetPage];
            dst.Annotations.Add(clone);

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Cloned annotation saved to '{outputPdf}'.");
    }

    // Example usage.
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_cloned.pdf";

        // Clone the first annotation on page 1, change its color to light blue, and place it on page 2.
        CloneAnnotationChangeColor(
            inputPdf: inputPath,
            sourcePage: 1,
            annotationIndex: 0,
            newColor: Color.LightBlue, // Aspose.Pdf.Color
            targetPage: 2,
            outputPdf: outputPath);
    }
}
