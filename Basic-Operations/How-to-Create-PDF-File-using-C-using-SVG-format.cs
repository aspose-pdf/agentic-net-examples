using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the source SVG and the target PDF
        const string svgPath = "input.svg";
        const string pdfPath = "output.pdf";

        // Verify that the SVG file exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG file into a Document using SvgLoadOptions
        // (SvgLoadOptions is in the Aspose.Pdf namespace)
        using (Document pdfDocument = new Document(svgPath, new SvgLoadOptions()))
        {
            // Save the document as PDF (no SaveOptions needed for PDF output)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"SVG has been converted to PDF: {pdfPath}");
    }
}