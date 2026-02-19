using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document and XslFoLoadOptions)

class XslFoToPdfConverter
{
    static void Main()
    {
        // Use the current working directory as a generic data folder.
        string dataDir = Directory.GetCurrentDirectory();

        // Path to the XSL‑FO file (input).
        string xslFoPath = Path.Combine(dataDir, "sample.fo");

        // Path where the resulting PDF will be saved (output).
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the input file exists before proceeding.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Input XSL‑FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // Initialize load options for XSL‑FO.
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            // Load the XSL‑FO document into an Aspose.Pdf Document object.
            using (Document pdfDocument = new Document(xslFoPath, loadOptions))
            {
                // Save the document as a regular PDF file.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF saved to: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}