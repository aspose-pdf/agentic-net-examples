using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - path to the source HTML file
        // args[1] - path to the output PDF/E-1 file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputHtmlPath> <outputPdfE1Path>");
            return;
        }

        string htmlPath = args[0];
        string outputPath = args[1];

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML file into a Document object
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            Document doc = new Document(htmlPath, loadOptions);

            // Convert the Document to PDF/E-1 format
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
            doc.Convert(conversionOptions);

            // Save the resulting PDF/E-1 document
            doc.Save(outputPath);
            Console.WriteLine($"Conversion successful. PDF/E-1 saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}