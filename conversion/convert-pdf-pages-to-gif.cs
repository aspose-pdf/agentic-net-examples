using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        const string pdfFile = @"YOUR_PDF_FILE";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize GifDevice with default resolution
            GifDevice gifDevice = new GifDevice();

            // Convert each page to a GIF image
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(dataDir, $"image{pageNumber}_out.gif");
                using (FileStream gifStream = new FileStream(outputPath, FileMode.Create))
                {
                    gifDevice.Process(pdfDocument.Pages[pageNumber], gifStream);
                }
                Console.WriteLine($"Saved GIF: {outputPath}");
            }
        }
    }
}