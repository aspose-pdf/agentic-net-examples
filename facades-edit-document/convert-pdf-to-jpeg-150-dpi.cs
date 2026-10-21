using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // JpegDevice configured with 150 DPI resolution and 90% quality.
            // The constructor sets the required parameters; older versions do not expose
            // writable Resolution, Compression, or Quality properties.
            var jpegDevice = new JpegDevice(new Resolution(150), 90);

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNum}.jpg");
                // Convert the current page to a JPEG image using Process(page, stream)
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
                Console.WriteLine($"Saved page {pageNum} as {outputPath}");
            }
        }
    }
}
