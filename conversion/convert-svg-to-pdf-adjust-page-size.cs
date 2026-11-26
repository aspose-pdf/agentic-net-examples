using System;
using System.IO;
using Aspose.Pdf; // Document, SvgLoadOptions, etc.

class Program
{
    static void Main()
    {
        const string svgPath   = "input.svg";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG with page size adjustment enabled
        SvgLoadOptions loadOptions = new SvgLoadOptions {
            AdjustPageSize = true
        };

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(svgPath, loadOptions))
        {
            // Save the loaded SVG as a PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"SVG converted to PDF: {pdfPath}");
    }
}