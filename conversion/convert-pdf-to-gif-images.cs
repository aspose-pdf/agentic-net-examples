using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "GifPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // GifDevice is a concrete implementation for GIF output; it does NOT implement IDisposable
                GifDevice gifDevice = new GifDevice();

                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    // Build the output GIF file name
                    string gifPath = Path.Combine(outputFolder, $"page_{pageNumber}.gif");

                    // Create a stream for the output file and let the device write to it
                    using (FileStream outStream = new FileStream(gifPath, FileMode.Create, FileAccess.Write))
                    {
                        gifDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                    }

                    Console.WriteLine($"Page {pageNumber} saved as GIF: {gifPath}");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // Image conversion may require GDI+ on non‑Windows platforms
            Console.WriteLine("Image conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
