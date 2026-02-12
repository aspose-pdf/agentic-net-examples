using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input SVG file path
        const string svgPath = "input.svg";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the SVG file exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        try
        {
            // Configure SVG loading options (adjust page size to match SVG dimensions)
            var loadOptions = new SvgLoadOptions
            {
                AdjustPageSize = true
            };

            // Load the SVG file into a new PDF document
            Document pdfDocument = new Document(svgPath, loadOptions);

            // Save the PDF document (uses the provided document-save rule)
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"SVG successfully converted to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}