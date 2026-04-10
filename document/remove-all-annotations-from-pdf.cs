using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clean_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Delete all annotations on the current page
                // AnnotationCollection.Delete() removes every annotation in the collection
                page.Annotations.Delete();
            }

            // Save the cleaned PDF (no annotations)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed. Clean PDF saved to '{outputPath}'.");
    }
}