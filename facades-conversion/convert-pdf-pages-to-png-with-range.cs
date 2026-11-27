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
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        // PdfConverter implements IDisposable, so wrap it in a using block as well
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Set conversion range and resolution (Resolution object, not an int)
            converter.StartPage = 3;   // first page to convert (1‑based)
            converter.EndPage   = 9;   // last page to convert (inclusive)
            converter.Resolution = new Resolution(300); // DPI

            // Prepare the conversion
            converter.DoConvert();

            // Convert each page in the specified range to a PNG file
            int totalPages = converter.EndPage - converter.StartPage + 1;
            for (int i = 0; i < totalPages; i++)
            {
                int pageNumber = converter.StartPage + i;
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // GetNextImage saves the current page as an image; format is inferred from the file extension
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
