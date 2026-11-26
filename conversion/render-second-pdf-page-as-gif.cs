using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputGifPath = "page2.gif";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least two pages
            if (pdfDoc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF does not contain a second page.");
                return;
            }

            // Set the desired resolution (150 DPI)
            Resolution resolution = new Resolution(150);

            // GifDevice does NOT implement IDisposable, so instantiate without a using block
            GifDevice gifDevice = new GifDevice(resolution);

            // Create the output file stream inside a using block (streams are disposable)
            using (FileStream outStream = new FileStream(outputGifPath, FileMode.Create))
            {
                // Render the second page (1‑based indexing) to the GIF stream
                gifDevice.Process(pdfDoc.Pages[2], outStream);
            }
        }

        Console.WriteLine($"Second page rendered to GIF at {outputGifPath}");
    }
}
