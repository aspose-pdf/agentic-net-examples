using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including XpsSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXpsPath = "output.xps";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // XpsSaveOptions with default settings
                XpsSaveOptions xpsOptions = new XpsSaveOptions();

                // Save as XPS; must pass the SaveOptions subclass because Document.Save(string) alone always writes PDF
                pdfDoc.Save(outputXpsPath, xpsOptions);
            }

            Console.WriteLine($"PDF successfully converted to XPS: {outputXpsPath}");
        }
        catch (Exception ex)
        {
            // Generic error handling – in production you might want more specific catches
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}