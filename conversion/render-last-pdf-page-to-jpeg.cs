using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "last_page.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has pages
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Aspose.Pdf uses 1‑based page indexing; get the last page
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Create a JPEG device with 600 DPI resolution
            JpegDevice jpegDevice = new JpegDevice(600);

            // Render the last page to a JPEG file
            jpegDevice.Process(lastPage, outputPath);
        }

        Console.WriteLine($"Last page rendered to JPEG at '{outputPath}'.");
    }
}