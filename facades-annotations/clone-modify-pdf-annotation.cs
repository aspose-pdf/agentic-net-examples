using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Annotation types

class PdfAnnotationHelper
{
    /// <summary>
    /// Clones an existing annotation on the specified page, modifies some of its properties,
    /// and adds the cloned annotation back to the same page.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    /// <param name="pageNumber">1‑based page number containing the annotation.</param>
    /// <param name="annotationIndex">Zero‑based index of the annotation on the page.</param>
    public static void CloneModifyAddAnnotation(string inputPdf, string outputPdf, int pageNumber, int annotationIndex)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        // Validate page number.
        if (pageNumber < 1 || pageNumber > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
            return;
        }

        Page page = doc.Pages[pageNumber];

        // Validate annotation index.
        if (annotationIndex < 0 || annotationIndex >= page.Annotations.Count)
        {
            Console.Error.WriteLine($"No annotation found at page {pageNumber}, index {annotationIndex}.");
            return;
        }

        // Retrieve the original annotation.
        Annotation original = page.Annotations[annotationIndex];

        // Clone the annotation. All concrete annotation types implement ICloneable via the Clone() method.
        Annotation clone = (Annotation)original.Clone();

        // Modify common properties.
        clone.Rect = original.Rect; // keep same rectangle (optional – you can change it here)
        clone.Name = original.Name;
        clone.Flags = original.Flags;
        clone.Color = original.Color;
        clone.Border = original.Border;
        clone.Contents = original.Contents;

        // Title exists only on markup‑type annotations.
        if (original is MarkupAnnotation originalMarkup && clone is MarkupAnnotation cloneMarkup)
        {
            cloneMarkup.Title = $"Cloned - {originalMarkup.Title}";
        }

        // Example visual change: make the border a bit thicker.
        if (clone.Border != null)
        {
            // Border.Width is an int, so we simply increment it.
            clone.Border.Width = clone.Border.Width + 1;
        }

        // Add the cloned annotation back to the same page.
        page.Annotations.Add(clone);

        // Save the modified PDF.
        doc.Save(outputPdf);

        Console.WriteLine($"Cloned annotation added and saved to '{outputPdf}'.");
    }

    // Example usage
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_cloned.pdf";
        const int pageNumber = 1;          // first page (1‑based)
        const int annotationIndex = 0;     // first annotation on the page (0‑based)

        CloneModifyAddAnnotation(inputPath, outputPath, pageNumber, annotationIndex);
    }
}
