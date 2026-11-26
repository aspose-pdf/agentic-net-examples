using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "EmfPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // EmfDevice renders a page to EMF at the specified DPI (200)
            // Devices are not disposable, so we instantiate it once and reuse it.
            var emfDevice = new EmfDevice(new Resolution(200));

            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string emfPath = Path.Combine(outputFolder, $"page_{pageNum}.emf");

                // Process the page and write the EMF file
                emfDevice.Process(pdfDoc.Pages[pageNum], emfPath);

                Console.WriteLine($"Saved page {pageNum} as EMF: {emfPath}");
            }
        }
    }
}
