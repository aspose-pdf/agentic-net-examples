using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain page 6.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page pageSix = doc.Pages[6];

            // Remove all RichMediaAnnotation objects from page 6
            // Create a snapshot of the annotations to avoid modifying the collection while iterating
            var richMediaAnnotations = pageSix.Annotations
                                             .OfType<RichMediaAnnotation>()
                                             .ToList();

            foreach (RichMediaAnnotation rma in richMediaAnnotations)
            {
                pageSix.Annotations.Remove(rma);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMediaAnnotations removed. Saved to '{outputPath}'.");
    }
}