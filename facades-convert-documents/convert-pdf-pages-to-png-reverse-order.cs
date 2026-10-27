using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "Pages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Define the resolution (DPI) for the PNG output – 300 DPI as an example
            const int dpi = 300;

            // PngDevice does NOT implement IDisposable, so instantiate it directly.
            // Use the Resolution constructor to set DPI and optionally enable transparency.
            var pngDevice = new PngDevice(new Resolution(dpi))
            {
                TransparentBackground = true // optional, can be omitted if not needed
            };

            // Process pages in reverse order (last page to first page)
            for (int pageNum = pdfDoc.Pages.Count; pageNum >= 1; pageNum--)
            {
                // Build the output file path
                string outPath = Path.Combine(outputFolder, $"Page_{pageNum}.png");

                // Convert the current page to PNG and write it to the file stream
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNum], imageStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to PNG images in reverse order.");
    }
}
