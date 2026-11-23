using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "SquaresRemoved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; page 4 is accessed via Pages[4]
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page pageFour = doc.Pages[4];

            // Collect all SquareAnnotation instances on page 4
            var squaresToRemove = new System.Collections.Generic.List<SquareAnnotation>();
            foreach (Annotation annot in pageFour.Annotations)
            {
                if (annot is SquareAnnotation square)
                {
                    squaresToRemove.Add(square);
                }
            }

            // Remove the collected square annotations from the page
            foreach (SquareAnnotation square in squaresToRemove)
            {
                pageFour.Annotations.Remove(square);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All SquareAnnotations on page 4 have been removed. Saved as '{outputPath}'.");
    }
}