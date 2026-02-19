using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source SVG file
        string svgPath = "input.svg";
        // Path where the resulting PDF will be saved
        string pdfPath = "output.pdf";

        // Ensure the SVG file exists before attempting to load it
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"Error: SVG file not found at '{svgPath}'.");
            return;
        }

        // Configure SVG loading options
        SvgLoadOptions loadOptions = new SvgLoadOptions
        {
            // Adjust the PDF page size to match the dimensions of the SVG
            AdjustPageSize = true
        };

        // Load the SVG file into a PDF document
        Document pdfDocument = new Document(svgPath, loadOptions);

        // Save the document as PDF (uses the provided document-save rule)
        pdfDocument.Save(pdfPath);

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}