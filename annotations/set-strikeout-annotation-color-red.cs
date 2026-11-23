using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "StrikesUpdated.pdf";
        const int targetPageNumber = 9; // pages are 1‑based

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Retrieve the page (1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // Iterate over annotations and process only StrikeOutAnnotation instances
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is StrikeOutAnnotation strikeOut)
                {
                    // Set the annotation color to red using Aspose.Pdf.Color (cross‑platform)
                    strikeOut.Color = Aspose.Pdf.Color.Red;
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}