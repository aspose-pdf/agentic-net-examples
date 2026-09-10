using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfFile = Path.Combine(dataDir, "input.pdf");

        // Desired output SVG file.
        string svgFile = Path.Combine(dataDir, "output.svg");

        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfFile}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfFile))
        {
            // Initialize SVG save options.
            SvgSaveOptions saveOptions = new SvgSaveOptions();

            // NOTE: SvgSaveOptions does not expose a property for CSS embedding,
            // because SVG output does not use external CSS. The option is kept
            // for API compatibility; no additional configuration is required.

            // Save the document as SVG.
            pdfDocument.Save(svgFile, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {svgFile}");
    }
}