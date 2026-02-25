using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types (Document, SvgLoadOptions, etc.) are in this namespace.

class Program
{
    static void Main()
    {
        // Input SVG file path – the vector graphic you want to turn into a PDF.
        const string svgPath = "input.svg";

        // Output PDF file path.
        const string pdfPath = "output.pdf";

        // Verify that the source SVG exists.
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG into an Aspose.Pdf Document.
        // The constructor that takes a file path and SvgLoadOptions parses the SVG
        // and creates a PDF document representation in memory.
        using (Document pdfDocument = new Document(svgPath, new SvgLoadOptions()))
        {
            // Save the in‑memory document as a PDF file.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"SVG has been converted to PDF and saved as '{pdfPath}'.");
    }
}