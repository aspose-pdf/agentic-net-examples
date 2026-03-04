using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string svgInputPath   = "input.svg";
        const string pdfOutputPath  = "output.pdf";

        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgInputPath}");
            return;
        }

        // Load the SVG file into a PDF Document (conversion)
        using (Document doc = new Document(svgInputPath, new SvgLoadOptions()))
        {
            // Create a PdfFileInfo facade bound to the document
            PdfFileInfo info = new PdfFileInfo(doc);

            // Set the desired PDF metadata
            info.Title    = "Sample SVG Conversion";
            info.Author   = "John Doe";
            info.Subject  = "SVG to PDF Example";
            info.Keywords = "SVG, PDF, Aspose.Pdf, Metadata";

            // Save the document with the updated metadata
            info.SaveNewInfo(pdfOutputPath);
        }

        Console.WriteLine($"SVG converted to PDF with metadata saved at '{pdfOutputPath}'.");
    }
}