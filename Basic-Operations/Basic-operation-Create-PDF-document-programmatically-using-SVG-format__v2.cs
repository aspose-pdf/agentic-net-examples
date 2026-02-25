using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string svgPath = "input.svg";
        const string pdfPath = "output.pdf";

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Configure SVG loading options (optional settings can be adjusted here)
        SvgLoadOptions loadOptions = new SvgLoadOptions
        {
            // Example: select the newer conversion engine
            // ConversionEngine = ConversionEngines.NewEngine,
            AdjustPageSize = true // adjust PDF page size to match SVG dimensions
        };

        // Load the SVG file into a PDF Document using the specified options
        using (Document doc = new Document(svgPath, loadOptions))
        {
            // Save the resulting document as PDF (default format)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF: {pdfPath}");
    }
}