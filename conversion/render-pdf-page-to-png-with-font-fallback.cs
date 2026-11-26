using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPngPath = "page6.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Register a fallback substitution so that any missing font is rendered with Times New Roman
            // The asterisk (*) matches any font name that cannot be found.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

            // Page indexing in Aspose.Pdf is 1‑based; page 6 is accessed via index 6
            if (pdfDoc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document has fewer than 6 pages.");
                return;
            }

            Page page = pdfDoc.Pages[6];

            // Set rendering resolution to 300 DPI
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page to a PNG file
            using (FileStream imageStream = new FileStream(outputPngPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }

            Console.WriteLine($"Page 6 rendered to PNG at 300 DPI: {outputPngPath}");
        }
    }
}
