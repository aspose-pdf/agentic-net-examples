using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jpegPath = "first_page.jpg";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Aspose.Pdf uses 1‑based page indexing; get the first page
            Page firstPage = pdfDoc.Pages[1];

            // Create the JPEG device (devices are not IDisposable, so do NOT use a using statement)
            // You can specify a resolution, quality, etc. here if desired.
            JpegDevice jpegDevice = new JpegDevice(new Resolution(300));

            // Create the output file stream and render the page
            using (FileStream outStream = new FileStream(jpegPath, FileMode.Create, FileAccess.Write))
            {
                jpegDevice.Process(firstPage, outStream);
            }
        }

        Console.WriteLine($"First page saved as JPEG to '{jpegPath}'.");
    }
}
