using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document; using ensures proper disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Define a high resolution (e.g., 300 DPI) for the PNG images.
            Resolution resolution = new Resolution(300);

            // Create a PNG device with the specified resolution.
            PngDevice pngDevice = new PngDevice(resolution);

            // Pages are 1‑based indexed.
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string pngPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Write each page to a separate PNG file.
                using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], pngStream);
                }

                Console.WriteLine($"Saved thumbnail: {pngPath}");
            }
        }
    }
}