using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Pages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a PNG device with the desired resolution (300 DPI).
            // Devices do NOT implement IDisposable, so do NOT wrap them in a using statement.
            var pngDevice = new PngDevice(new Resolution(300));

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save the page as a PNG image. Only the stream needs to be disposed.
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG: {outputPath}");
            }
        }
    }
}
