using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution class resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter implements IDisposable – wrap it in a using block.
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF document.
                converter.BindPdf(inputPdfPath);

                // Set the desired resolution (DPI) for the output images.
                // This will affect all pages converted thereafter.
                converter.Resolution = new Resolution(200);

                // Get total page count after binding.
                int totalPages = converter.PageCount;

                // Convert each page individually to a separate TIFF file.
                for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    // Define the range – only the current page.
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;

                    // Perform the conversion for the selected page.
                    converter.DoConvert();

                    string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.tiff");

                    // Save the converted page as a TIFF image.
                    // PdfConverter.SaveAsTIFF writes the selected pages (here only one) to the file.
                    converter.SaveAsTIFF(outputPath);

                    Console.WriteLine($"Page {pageNumber} saved as TIFF: {outputPath}");
                }
            }

            Console.WriteLine("All pages have been successfully converted to TIFF images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
