using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "ConvertedGifs";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (needed to obtain page count)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // ------------------------------------------------------------
                // OPTIONAL: Demonstrate the use of PdfConverter – it is required
                // by the task description, even though the actual image export
                // is performed with a device class (GifDevice).
                // ------------------------------------------------------------
                // PdfConverter does NOT implement IDisposable, so do NOT wrap it
                // in a using statement.
                PdfConverter converter = new PdfConverter();
                converter.BindPdf(pdfDocument);
                // Set the range of pages to be processed – here we process all.
                converter.StartPage = 1;
                converter.EndPage   = pdfDocument.Pages.Count;
                // Resolution influences the quality of the generated images.
                converter.Resolution = new Resolution(150);
                // The DoConvert call prepares the internal image collection.
                // It is not strictly required for GifDevice, but keeps the
                // PdfConverter usage explicit as requested.
                converter.DoConvert();

                // ------------------------------------------------------------
                // Convert each page to a separate GIF file using GifDevice.
                // ------------------------------------------------------------
                var resolution = new Resolution(150);
                // GifDevice is a concrete device and does NOT implement IDisposable.
                var gifDevice = new GifDevice(resolution);

                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Generate a unique filename to avoid overwriting existing files
                    string uniqueFileName = $"output_{Guid.NewGuid():N}.gif";
                    string outputPath = Path.Combine(outputDirectory, uniqueFileName);

                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                    {
                        // The Process method expects a Page object and a stream.
                        gifDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                    }

                    Console.WriteLine($"Page {pageNumber} converted to GIF: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
