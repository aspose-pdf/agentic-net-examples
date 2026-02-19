using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define paths (adjust as needed)
        string dataDir = @"YOUR_DATA_DIRECTORY";
        string xpsPath = Path.Combine(dataDir, "input.xps");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the XPS source file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"Error: XPS file not found at '{xpsPath}'.");
            return;
        }

        try
        {
            // Load XPS file with default options
            XpsLoadOptions loadOptions = new XpsLoadOptions();
            using (Document pdfDocument = new Document(xpsPath, loadOptions))
            {
                // Save as regular PDF (non‑PDF/A)
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}