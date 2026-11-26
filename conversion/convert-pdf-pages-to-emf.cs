using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "EmfPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    Page page = pdfDoc.Pages[pageNumber];

                    // EmfDevice with default resolution (no parameters)
                    EmfDevice emfDevice = new EmfDevice();

                    // Build output file name
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.emf");

                    // Render the current page directly to the EMF file
                    emfDevice.Process(page, outputPath);

                    Console.WriteLine($"Saved page {pageNumber} as EMF: {outputPath}");
                }
            }

            Console.WriteLine("PDF to EMF conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
