using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class PdfAnnotationExtractor
{
    /// <summary>
    /// Extracts every annotation rectangle from the input PDF and creates a separate
    /// one‑page PDF for each rectangle. The generated pages are then concatenated
    /// into a single PDF for visual inspection.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the result PDF will be saved.</param>
    public static void ExtractAnnotationsToPages(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // List to hold temporary one‑page PDFs.
        List<string> tempFiles = new List<string>();

        // Load the source document.
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
            {
                Page srcPage = srcDoc.Pages[pageIndex];

                // Iterate over each annotation on the current page.
                foreach (Annotation annot in srcPage.Annotations)
                {
                    // Create a temporary one‑page document that will contain a visual
                    // representation of the annotation rectangle.
                    using (Document tempDoc = new Document())
                    {
                        // Add a new page with the same size as the source page.
                        Page newPage = tempDoc.Pages.Add();
                        newPage.PageInfo.Width = srcPage.PageInfo.Width;
                        newPage.PageInfo.Height = srcPage.PageInfo.Height;

                        // The annotation rectangle is an Aspose.Pdf.Rectangle.
                        // Use the fully qualified name to avoid ambiguity.
                        Aspose.Pdf.Rectangle rect = annot.Rect; // <-- Fixed property name

                        // Add a square annotation (red border) to visualize the rectangle.
                        SquareAnnotation visual = new SquareAnnotation(newPage, rect);
                        visual.Color = Aspose.Pdf.Color.Red;
                        visual.Opacity = 0.5; // semi‑transparent for easier viewing
                        newPage.Annotations.Add(visual);

                        // Save the temporary one‑page PDF to a unique file.
                        string tempPath = Path.Combine(Path.GetTempPath(),
                                                       Guid.NewGuid().ToString() + ".pdf");
                        tempDoc.Save(tempPath);
                        tempFiles.Add(tempPath);
                    }
                }
            }
        }

        // If no annotations were found, inform the user and exit.
        if (tempFiles.Count == 0)
        {
            Console.WriteLine("No annotations found in the source PDF.");
            return;
        }

        // Use Aspose.Pdf.Facades.PdfFileEditor to concatenate all temporary PDFs
        // into the final output PDF.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(tempFiles.ToArray(), outputPdfPath);

        // Clean up temporary files.
        foreach (string file in tempFiles)
        {
            try { File.Delete(file); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"Annotation pages saved to: {outputPdfPath}");
    }

    // Example usage.
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotations_visual.pdf";

        ExtractAnnotationsToPages(inputPath, outputPath);
    }
}
