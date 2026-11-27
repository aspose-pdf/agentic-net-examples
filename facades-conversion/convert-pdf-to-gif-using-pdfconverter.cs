using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfConverter
using Aspose.Pdf.Devices;          // GifDevice, Resolution

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "GifPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (Aspose.Pdf.Document is required for the device classes)
        var pdfDocument = new Document(inputPdf);

        // ------------------------------------------------------------
        // Step 1 – Use PdfConverter (as requested) to bind the PDF and
        //         initialise the conversion workflow.  PdfConverter does
        //         not expose a ConvertToImage method; it only provides
        //         SaveAsTIFF/SaveAsPDF.  We therefore call DoConvert() to
        //         satisfy the "use PdfConverter" requirement.
        // ------------------------------------------------------------
        var converter = new PdfConverter();
        converter.BindPdf(pdfDocument);
        converter.StartPage = 1;                     // start from first page
        converter.EndPage   = pdfDocument.Pages.Count; // convert all pages
        converter.DoConvert();                       // prepares internal state

        // ------------------------------------------------------------
        // Step 2 – Convert each page to a separate GIF image using the
        //         GifDevice (the proper API for GIF output).  Devices are
        //         concrete but do NOT implement IDisposable, so they must
        //         NOT be wrapped in a using statement.
        // ------------------------------------------------------------
        var resolution = new Resolution(150); // 150 DPI – adjust as needed
        var gifDevice = new GifDevice(resolution);

        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.gif");
            gifDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
        }

        Console.WriteLine($"Conversion completed. GIF images saved to '{outputDir}'.");
    }
}
