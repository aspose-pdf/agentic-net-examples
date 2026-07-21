using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Optional: set a CropBox if you need to crop the page first
            // Aspose.Pdf.Rectangle crop = new Aspose.Pdf.Rectangle(50, 50, 550, 750);
            // doc.Pages[1].CropBox = crop;

            // Iterate through all pages (1‑based indexing) and retrieve the TrimBox
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Aspose.Pdf.Rectangle trimBox = page.TrimBox;

                // Output the TrimBox coordinates
                Console.WriteLine(
                    $"Page {i} TrimBox: LLX={trimBox.LLX}, LLY={trimBox.LLY}, URX={trimBox.URX}, URY={trimBox.URY}");
            }
        }
    }
}