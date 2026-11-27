using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution and GetNextImage overload

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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        // Create and configure the PdfConverter (also disposable)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded PDF to the converter
            converter.BindPdf(pdfDoc);

            // Set conversion range and resolution (Resolution object, not int)
            converter.StartPage = 1;               // first page to convert
            converter.EndPage   = 5;               // last page to convert
            converter.Resolution = new Resolution(200); // DPI

            // Prepare the conversion
            converter.DoConvert();

            // Iterate over the specified page range and save each page as a PNG image
            for (int pageNum = converter.StartPage; pageNum <= converter.EndPage; pageNum++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.png");
                // GetNextImage(string) infers the image format from the file extension (PNG here)
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Saved page {pageNum} as PNG to '{outputPath}'.");
            }

            // Unbind the PDF (optional, as disposing will clean up)
            converter.Close();
        }
    }
}
