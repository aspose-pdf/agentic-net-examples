using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source XSL‑FO file.
        string dataDir = "Data";

        // Path to the XSL‑FO file (input).
        string xslFoPath = Path.Combine(dataDir, "input.fo");

        // Path where the resulting PDF will be saved (output).
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the input file exists before proceeding.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Error: XSL‑FO file not found at '{xslFoPath}'.");
            return;
        }

        try
        {
            // Create load options for XSL‑FO. No additional settings are required
            // for a basic conversion, but the object can be customized if needed.
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            // Load the XSL‑FO document into an Aspose.Pdf Document instance.
            using (Document pdfDocument = new Document(xslFoPath, loadOptions))
            {
                // Save the document as a regular PDF (non‑PDF/A) using the simple
                // Document.Save method as prescribed by the lifecycle rules.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}