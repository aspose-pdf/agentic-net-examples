using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJpegPath = "cover.jpg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Verify that the document has at least one page
                if (pdfDoc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("The PDF does not contain any pages.");
                    return;
                }

                // Create a JPEG device with desired resolution and maximum quality (100)
                // JpegDevice does NOT implement IDisposable, so do NOT wrap it in a using statement.
                var jpegDevice = new JpegDevice(new Resolution(300), 100);

                // Render the first (cover) page to a JPEG file
                jpegDevice.Process(pdfDoc.Pages[1], outputJpegPath);
            }

            Console.WriteLine($"Cover page saved as high‑quality JPEG: {outputJpegPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
