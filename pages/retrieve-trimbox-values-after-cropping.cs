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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Example: apply a crop to the first page (optional)
            // Here we set the CropBox to a smaller rectangle.
            // Comment out if cropping is not required.
            Aspose.Pdf.Rectangle newCrop = new Aspose.Pdf.Rectangle(50, 50, 550, 750);
            doc.Pages[1].CropBox = newCrop;

            // After cropping, retrieve the TrimBox of each page.
            // TrimBox defines the intended final size of the page after trimming.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Aspose.Pdf.Rectangle trimBox = page.TrimBox;

                // If TrimBox is not set, it defaults to MediaBox.
                // Output the coordinates for verification.
                Console.WriteLine($"Page {i} TrimBox: LLX={trimBox.LLX}, LLY={trimBox.LLY}, URX={trimBox.URX}, URY={trimBox.URY}");
            }
        }
    }
}