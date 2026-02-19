using System;
using System.IO;
using Aspose.Pdf; // XslFoLoadOptions resides directly in this namespace

class Program
{
    static void Main()
    {
        try
        {
            // Use the application base directory for demo files
            string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            string xslFoPath = Path.Combine(dataDir, "sample.fo");
            string outputPdfPath = Path.Combine(dataDir, "output-pdf-e1.pdf");

            // Verify that the XSL‑FO file exists
            if (!File.Exists(xslFoPath))
            {
                Console.Error.WriteLine($"Error: XSL‑FO file not found at '{xslFoPath}'.");
                return;
            }

            // Initialize load options for XSL‑FO
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            // Load the XSL‑FO document into an Aspose.Pdf Document object
            using (Document pdfDocument = new Document(xslFoPath, loadOptions))
            {
                // Save the resulting PDF (standard PDF; PDF/E‑1 would require additional options)
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
