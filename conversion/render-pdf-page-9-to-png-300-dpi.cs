using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputPngPath = "page9.png";   // rendered image

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the PDF has at least 9 pages (Aspose.Pdf uses 1‑based indexing)
            if (pdfDoc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // Create a PNG device with 300 DPI resolution
            PngDevice pngDevice = new PngDevice(new Resolution(300));

            // Render page 9 to a PNG file
            using (FileStream outStream = new FileStream(outputPngPath, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(pdfDoc.Pages[9], outStream);
            }
        }

        Console.WriteLine($"Page 9 rendered to PNG at 300 DPI: {outputPngPath}");
    }
}