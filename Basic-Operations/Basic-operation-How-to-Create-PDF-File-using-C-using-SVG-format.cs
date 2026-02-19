using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string dataDir = "YOUR_DATA_DIRECTORY";
        string svgFile = Path.Combine(dataDir, "input.svg");
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify SVG source exists
        if (!File.Exists(svgFile))
        {
            Console.Error.WriteLine($"SVG file not found: {svgFile}");
            return;
        }

        try
        {
            // Load SVG with default options (you can adjust ConversionEngine if required)
            SvgLoadOptions loadOptions = new SvgLoadOptions();
            // Example: use the new engine
            // loadOptions.ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine;

            // Create a PDF document from the SVG
            Document pdfDocument = new Document(svgFile, loadOptions);

            // Save the document as PDF
            pdfDocument.Save(pdfFile);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}