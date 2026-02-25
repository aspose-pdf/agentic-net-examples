using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, SvgLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input SVG file path and desired output PDF file path.
        const string svgPath   = "input.svg";
        const string pdfPath   = "output.pdf";

        // Verify that the SVG source file exists.
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG into a PDF Document using SvgLoadOptions.
        // The default conversion engine (LegacyEngine) is used; you can switch to NewEngine if needed:
        // var loadOptions = new SvgLoadOptions { ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine };
        var loadOptions = new SvgLoadOptions(); // default options

        // Wrap the Document in a using block for deterministic disposal (lifecycle rule).
        using (Document pdfDoc = new Document(svgPath, loadOptions))
        {
            // Save the document as PDF. No SaveOptions are required because the target format is PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"SVG has been converted to PDF and saved as '{pdfPath}'.");
    }
}