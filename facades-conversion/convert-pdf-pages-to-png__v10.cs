using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PagesAsPng";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            var pdfDocument = new Document(inputPdfPath);

            // Use PdfConverter to bind the document (required by the task) and obtain page count
            using (var converter = new PdfConverter())
            {
                converter.BindPdf(pdfDocument);
                int pageCount = pdfDocument.Pages.Count; // or converter.PageCount after binding

                // Iterate through all pages and convert each to PNG using PngDevice
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                    using (var imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        // PngDevice handles the rasterization of a single page
                        var pngDevice = new PngDevice(new Resolution(150));
                        pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    }

                    Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
                }

                // Release resources held by the converter
                converter.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
