using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "LinesStyleChanged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 8 is accessed via index 8
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            Page pageEight = doc.Pages[8];

            // Iterate over all annotations on page 8
            foreach (Annotation annot in pageEight.Annotations)
            {
                // Process only line annotations
                if (annot is LineAnnotation lineAnnot)
                {
                    // Set the starting line style to Square
                    lineAnnot.StartingStyle = LineEnding.Square;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
