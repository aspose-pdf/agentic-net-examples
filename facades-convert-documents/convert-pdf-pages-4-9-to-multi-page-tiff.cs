using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a PdfConverter facade (belongs to Aspose.Pdf.Facades)
            PdfConverter converter = new PdfConverter();

            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Set the page range to convert (Aspose.Pdf uses 1‑based indexing)
            converter.StartPage = 4;
            converter.EndPage   = 9;

            // Perform the conversion preparation
            converter.DoConvert();

            // Save the selected pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF image created at: {outputPath}");
    }
}