using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input SVG file path
        const string svgPath = "input.svg";
        // Desired output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the SVG source exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG into a PDF Document using SvgLoadOptions
        SvgLoadOptions loadOptions = new SvgLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document pdfDoc = new Document(svgPath, loadOptions))
        {
            // Save the document as a PDF file
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF: {pdfPath}");
    }
}