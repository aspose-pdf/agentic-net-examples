using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page27.png";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Register Comic Sans MS as a fallback for any missing font
        // The asterisk (*) means "any font that cannot be found".
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Comic Sans MS"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains at least 27 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 27)
            {
                Console.Error.WriteLine("The document has fewer than 27 pages.");
                return;
            }

            // Retrieve the 27th page
            Page page = doc.Pages[27];

            // Create a PNG device with a resolution of 200 DPI
            Resolution resolution = new Resolution(200);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page to PNG and write to the output file
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Page 27 has been saved as PNG to '{outputPath}'.");
    }
}
