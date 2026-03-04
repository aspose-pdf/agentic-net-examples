using System;
using System.IO;
using Aspose.Pdf;   // SvgSaveOptions resides in this namespace

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the SVG will be written
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file path
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Desired output SVG file path
        string svgPath = Path.Combine(dataDir, "output.svg");

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize SVG save options (required to force SVG output)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the document as SVG using the options
            pdfDocument.Save(svgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {svgPath}");
    }
}