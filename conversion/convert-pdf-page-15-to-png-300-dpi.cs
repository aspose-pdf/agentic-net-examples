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
        const string outputPng = "page15.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Register Arial as a fallback font for any missing fonts in the PDF
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Verify the document contains at least 15 pages (Aspose.Pdf uses 1‑based indexing)
            if (pdfDoc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The PDF has fewer than 15 pages.");
                return;
            }

            // Retrieve the 15th page
            Page page = pdfDoc.Pages[15];

            // Create a PNG device with a resolution of 300 DPI
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the page to a PNG file
            using (FileStream imageStream = new FileStream(outputPng, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Page 15 has been saved as PNG to '{outputPng}'.");
    }
}
