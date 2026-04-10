using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat enum
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution class

class PdfToBmpConverter
{
    static void Main()
    {
        // Directory that contains the source PDF and where BMP files will be written
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Full path to the input PDF file
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Ensure the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(pdfDocument);

            // Set a higher resolution for better image quality (optional)
            converter.Resolution = new Resolution(300);

            // Font substitution is enabled by default in the current Aspose.PDF API.
            // If a newer version exposes a specific property (e.g., FontSubstitutionEnabled),
            // it can be set here. The previous "FontSubstitution" property no longer exists.

            // Prepare the converter for processing
            converter.DoConvert();

            // Extract each page as a BMP image
            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string bmpPath = Path.Combine(dataDir, $"page_{pageNumber}_out.bmp");
                // Use System.Drawing.Imaging.ImageFormat for BMP output
                converter.GetNextImage(bmpPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF conversion to BMP completed.");
    }
}
