using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfConverter converter = new PdfConverter(pdfDoc))
        {
            // Set the desired resolution (300 DPI)
            converter.Resolution = new Resolution(300);

            // Convert all pages
            converter.StartPage = 1;
            converter.EndPage   = pdfDoc.Pages.Count;
            converter.DoConvert();

            // Save each page as a PNG image (format inferred from file extension)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                converter.GetNextImage(outputPath);
            }
        }

        Console.WriteLine($"PDF pages have been converted to PNG images at 300 DPI in folder: {outputFolder}");
    }
}
