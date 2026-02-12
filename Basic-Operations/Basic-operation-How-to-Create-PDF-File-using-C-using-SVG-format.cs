using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        string svgPath = "input.svg";
        string pdfPath = "output.pdf";

        // Validate input file
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        try
        {
            // Load SVG into a PDF document using SvgLoadOptions
            var loadOptions = new SvgLoadOptions
            {
                // Adjust the PDF page size to match the SVG dimensions
                AdjustPageSize = true
            };

            // Create the PDF document from the SVG file
            Document pdfDocument = new Document(svgPath, loadOptions);

            // Save the resulting PDF
            // document-save rule applied here
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"PDF created successfully at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF creation: {ex.Message}");
        }
    }
}