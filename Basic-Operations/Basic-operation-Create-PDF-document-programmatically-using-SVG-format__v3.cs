using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string svgInputPath = "input.svg";
        const string pdfOutputPath = "output.pdf";

        // Verify that the SVG file exists before proceeding.
        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"Error: SVG file not found at '{svgInputPath}'.");
            return;
        }

        // Load the SVG file using the dedicated SvgLoadOptions.
        // This creates a PDF document where each SVG element becomes a PDF page element.
        using (Document pdfDoc = new Document(svgInputPath, new SvgLoadOptions()))
        {
            // Save the resulting document as PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Successfully converted '{svgInputPath}' to PDF at '{pdfOutputPath}'.");
    }
}