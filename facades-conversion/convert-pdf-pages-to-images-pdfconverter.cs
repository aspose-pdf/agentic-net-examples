using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PDF converter (Facades API)
            PdfConverter converter = new PdfConverter();

            // Bind the loaded document to the converter
            converter.BindPdf(doc);

            // Specify the page range (pages 2 through 5) and resolution (144 DPI)
            converter.StartPage = 2;
            converter.EndPage   = 5;
            converter.Resolution = new Resolution(144);

            // Perform the conversion (DoConvert returns void)
            converter.DoConvert();

            // Save each converted page as a separate TIFF file.
            // Using TIFF because PdfConverter does not provide a SaveAsPNG method.
            // The placeholder {0} will be replaced with the page number.
            string outputPattern = Path.Combine(outputFolder, "page_{0}.tiff");
            converter.SaveAsTIFF(outputPattern);

            Console.WriteLine($"Pages 2‑5 have been saved as TIFF images in '{outputFolder}'.");

            // Release resources used by the converter
            converter.Close();
        }
    }
}
