using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page20.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Register a fallback to Times New Roman for any missing font.
        // The asterisk (*) means "any font name"; the second argument is the fallback font name.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least 20 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 20)
            {
                Console.Error.WriteLine("The document contains fewer than 20 pages.");
                return;
            }

            // Retrieve the 20th page
            Page page = doc.Pages[20];

            // Create a PNG device with a resolution of 300 DPI
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page to a PNG file
            pngDevice.Process(page, outputPng);
        }

        Console.WriteLine($"Page 20 rendered to PNG at 300 DPI: {outputPng}");
    }
}
