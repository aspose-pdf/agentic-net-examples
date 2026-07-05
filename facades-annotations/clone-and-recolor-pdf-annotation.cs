using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationCloner
{
    /// <summary>
    /// Clones the first annotation found on the source page, changes its color,
    /// and adds the cloned annotation to the target page.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    /// <param name="sourcePage">1‑based index of the page containing the original annotation.</param>
    /// <param name="targetPage">1‑based index of the page where the cloned annotation will be placed.</param>
    /// <param name="newColor">The color to apply to the cloned annotation.</param>
    public static void CloneAndRecolor(string inputPdf, string outputPdf,
                                       int sourcePage, int targetPage,
                                       Color newColor)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Validate page indices (Aspose.Pdf uses 1‑based indexing)
            if (sourcePage < 1 || sourcePage > doc.Pages.Count ||
                targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page numbers.");
                return;
            }

            Page srcPage = doc.Pages[sourcePage];
            Page dstPage = doc.Pages[targetPage];

            // Ensure the source page has at least one annotation
            if (srcPage.Annotations.Count == 0)
            {
                Console.Error.WriteLine($"No annotations found on page {sourcePage}.");
                return;
            }

            // Take the first annotation as the example to clone
            Annotation original = srcPage.Annotations[1];

            // Determine the type of the original annotation.
            // For this example we handle SquareAnnotation and StampAnnotation.
            // Additional types can be added as needed.
            Annotation clone = null;

            if (original is SquareAnnotation square)
            {
                // Create a new SquareAnnotation on the target page with the same rectangle
                clone = new SquareAnnotation(dstPage, square.Rect)
                {
                    Color = newColor,
                    Contents = square.Contents,
                    Border = square.Border,
                    Name = square.Name
                };
            }
            else if (original is StampAnnotation stamp)
            {
                clone = new StampAnnotation(dstPage, stamp.Rect)
                {
                    Color = newColor,
                    Contents = stamp.Contents,
                    Border = stamp.Border,
                    Name = stamp.Name,
                    Icon = stamp.Icon
                };
            }
            else
            {
                // Fallback: create a generic StampAnnotation (works for most visual annotations)
                clone = new StampAnnotation(dstPage, original.Rect)
                {
                    Color = newColor,
                    Contents = original.Contents,
                    Border = original.Border,
                    Name = original.Name
                };
            }

            // Add the cloned annotation to the target page's annotation collection
            dstPage.Annotations.Add(clone);

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Cloned annotation saved to '{outputPdf}'.");
    }

    // Example usage
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_cloned.pdf";

        // Clone annotation from page 1 to page 2 and recolor it to red
        CloneAndRecolor(inputPath, outputPath, sourcePage: 1, targetPage: 2, Color.Red);
    }
}