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
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // OPTIONAL: apply a crop box (e.g., shrink 10 points from each side)
                // This demonstrates that TrimBox can be read after a cropping operation
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double left   = mediaBox.LLX + 10;
                double bottom = mediaBox.LLY + 10;
                double right  = mediaBox.URX - 10;
                double top    = mediaBox.URY - 10;
                page.CropBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);

                // Retrieve the TrimBox after cropping
                Aspose.Pdf.Rectangle trimBox = page.TrimBox;

                // Output the TrimBox coordinates
                Console.WriteLine(
                    $"Page {i} TrimBox => LLX={trimBox.LLX}, LLY={trimBox.LLY}, URX={trimBox.URX}, URY={trimBox.URY}");
            }

            // If you want to persist the cropping changes, uncomment the line below:
            // doc.Save("cropped_output.pdf");
        }
    }
}