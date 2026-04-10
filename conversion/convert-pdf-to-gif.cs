// Program.cs – PDF to GIF conversion using Aspose.Pdf
// NOTE: To resolve the build error "Cannot create a file when that file already exists" caused by the automatic generation of GlobalUsings.g.cs,
// add the following property to your .csproj file (or edit the existing one):
//   <GenerateGlobalUsings>false</GenerateGlobalUsings>
// This disables the SDK's GlobalUsings generation and eliminates the file‑collision error.

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing the PDF file – replace with your actual path
        const string dataDir = @"YOUR_DATA_DIRECTORY";
        // PDF file name – replace with your actual file name
        const string pdfFile = @"YOUR_PDF_FILE";

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a GifDevice with default parameters (default resolution)
            GifDevice gifDevice = new GifDevice();

            // Pages are 1‑based; iterate through each page
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outPath = Path.Combine(dataDir, $"image{pageNumber}_out.gif");
                // Create a file stream for the output GIF
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the current page to GIF and write to the stream
                    gifDevice.Process(pdfDocument.Pages[pageNumber], outStream);
                }
                Console.WriteLine($"Saved GIF: {outPath}");
            }
        }
    }
}
