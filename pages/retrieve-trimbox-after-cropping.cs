using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cropped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; work with the first page as an example
            Page page = doc.Pages[1];

            // OPTIONAL: define a new CropBox to crop the page content
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle newCrop = new Aspose.Pdf.Rectangle(50, 50, 500, 750);
            page.CropBox = newCrop;

            // Retrieve the TrimBox after cropping
            Aspose.Pdf.Rectangle trimBox = page.TrimBox;

            // Output TrimBox coordinates
            Console.WriteLine($"TrimBox: LLX={trimBox.LLX}, LLY={trimBox.LLY}, URX={trimBox.URX}, URY={trimBox.URY}");

            // Save the (potentially) cropped document
            doc.Save(outputPath);
        }

        Console.WriteLine("Processing completed.");
    }
}