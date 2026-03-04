using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types (Document, SvgLoadOptions, etc.) are in this namespace

class Program
{
    static void Main()
    {
        // Directory containing the source SVG file and where the PDF will be saved
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input SVG file path
        string svgFile = Path.Combine(dataDir, "input.svg");

        // Output PDF file path
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the SVG source exists
        if (!File.Exists(svgFile))
        {
            Console.Error.WriteLine($"SVG file not found: {svgFile}");
            return;
        }

        // Load the SVG using SvgLoadOptions (no extra using directives for LoadOptions)
        SvgLoadOptions loadOptions = new SvgLoadOptions();

        // Use a using block to ensure the Document is disposed properly
        using (Document pdfDocument = new Document(svgFile, loadOptions))
        {
            // Save the loaded content as a PDF file
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"SVG successfully converted to PDF: {pdfFile}");
    }
}