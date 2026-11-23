using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "HighlightsModified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 2 is accessed with index 2
            Page pageTwo = doc.Pages[2];

            // Iterate over all annotations on page 2
            foreach (Annotation annot in pageTwo.Annotations)
            {
                // Process only HighlightAnnotation objects
                if (annot is HighlightAnnotation highlight)
                {
                    // Change the highlight color to orange.
                    // Use Aspose.Pdf.Color (cross‑platform) – orange defined via RGB.
                    highlight.Color = Aspose.Pdf.Color.FromRgb(1.0, 0.647, 0.0); // Orange
                }
            }

            // Save the modified PDF. No SaveOptions needed because we are saving as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}