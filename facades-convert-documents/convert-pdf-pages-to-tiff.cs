using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);
            int pageCount = pdfDocument.Pages.Count;

            // Iterate through each page and render it as a separate TIFF image
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");

                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // TiffDevice renders a single page to the provided stream.
                    // Resolution can be adjusted as needed (e.g., 300 DPI).
                    TiffDevice tiffDevice = new TiffDevice(new Resolution(300));
                    tiffDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }
            }

            Console.WriteLine("PDF successfully converted to separate TIFF images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
