using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Pages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Create a PNG device with default resolution (no explicit settings)
                PngDevice pngDevice = new PngDevice();

                // Build the output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.png");

                // Save the page as PNG using a FileStream inside a using block
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(page, imageStream);
                }

                Console.WriteLine($"Saved page {pageNum} as PNG → {outputPath}");
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}