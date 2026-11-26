using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "first_page.bmp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Verify that the document has at least one page (pages are 1‑based)
                if (pdfDoc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("PDF contains no pages.");
                    return;
                }

                // Create the output BMP file stream inside a using block
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // BmpDevice renders a PDF page to BMP format
                    BmpDevice bmpDevice = new BmpDevice();

                    // Process the first page (index 1) and write the BMP data to the stream
                    bmpDevice.Process(pdfDoc.Pages[1], bmpStream);
                }
            }

            Console.WriteLine($"First page saved as BMP to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}