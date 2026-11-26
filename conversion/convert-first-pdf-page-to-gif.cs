using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputGif = "output.gif";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Verify that the document contains at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("PDF contains no pages.");
                return;
            }

            // Create a GifDevice with the desired resolution (e.g., 150 DPI)
            var gifDevice = new GifDevice(new Resolution(150));

            // Create the output file stream
            using (FileStream outStream = File.Create(outputGif))
            {
                // Process the first page and write the GIF to the stream
                gifDevice.Process(pdfDoc.Pages[1], outStream);
            }
        }

        Console.WriteLine($"First page saved as GIF: {outputGif}");
    }
}
