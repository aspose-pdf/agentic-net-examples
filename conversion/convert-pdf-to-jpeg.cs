using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Required for JpegDevice

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "JpegPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use JpegDevice which applies the default JPEG quality settings.
            // This replaces JpegSaveOptions (which was not available in the referenced assembly).
            var jpegDevice = new JpegDevice(); // default quality, default resolution

            // Iterate through each page and save it as a separate JPEG file.
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine($"PDF pages have been saved as JPEG images in '{outputFolder}'.");
    }
}
