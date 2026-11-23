using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 5; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Create a sticky note (text annotation) on the specified page
            Page page = doc.Pages[pageNumber];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a sticky annotation added on page 5.",
                Open     = true,                     // open by default
                Icon     = TextIcon.Note               // sticky‑note icon
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sticky annotation added and saved to '{outputPath}'.");
    }
}