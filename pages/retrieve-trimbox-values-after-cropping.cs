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

        // Load the PDF document (using rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing per rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // OPTIONAL: adjust CropBox if you need to crop the page.
                // Uncomment and modify the rectangle as required.
                // Aspose.Pdf.Rectangle newCrop = new Aspose.Pdf.Rectangle(
                //     50, 50,
                //     page.MediaBox.URX - 50,
                //     page.MediaBox.URY - 50);
                // page.CropBox = newCrop;

                // Retrieve the TrimBox after any cropping
                Aspose.Pdf.Rectangle trimBox = page.TrimBox;

                // Output the TrimBox coordinates
                Console.WriteLine(
                    $"Page {i} TrimBox => LLX={trimBox.LLX}, LLY={trimBox.LLY}, URX={trimBox.URX}, URY={trimBox.URY}");
            }
        }
    }
}