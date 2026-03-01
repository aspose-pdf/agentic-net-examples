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

        // Load the SVG file into a Document object
        using (Document pdfDocument = new Document(svgPath, new SvgLoadOptions()))
        {
            // Save the document as a PDF file
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"SVG has been converted to PDF: '{pdfPath}'");
    }
}