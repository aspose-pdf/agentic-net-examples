using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the SVG file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input SVG file path.
        string svgPath = Path.Combine(dataDir, "input.svg");

        // Desired output PDF file path.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the SVG source exists.
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG into a Document using SvgLoadOptions.
        using (Document pdfDoc = new Document(svgPath, new SvgLoadOptions()))
        {
            // Save the document as a PDF file.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF: {pdfPath}");
    }
}