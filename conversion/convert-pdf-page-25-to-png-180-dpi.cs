using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPngPath = "page25.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Register Arial as a fallback for any missing fonts during rendering
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 25 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 25)
            {
                Console.Error.WriteLine("The document contains fewer than 25 pages.");
                return;
            }

            // Retrieve the 25th page (1‑based index)
            Page page = doc.Pages[25];

            // Create a PNG device with 180 DPI resolution
            Resolution resolution = new Resolution(180); // both horizontal and vertical DPI are 180
            PngDevice pngDevice = new PngDevice(resolution);

            // Save the page as a PNG image
            using (FileStream imageStream = new FileStream(outputPngPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Page 25 has been saved as PNG to '{outputPngPath}'.");
    }
}
