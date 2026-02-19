using System;
using System.IO;
using Aspose.Pdf;          // Document, XpsLoadOptions

class Program
{
    static void Main()
    {
        // Directory that contains the source XPS file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input XPS file path.
        string xpsFile = Path.Combine(dataDir, "sample.xps");

        // Output PDF file path.
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        // Verify that the XPS source file exists.
        if (!File.Exists(xpsFile))
        {
            Console.Error.WriteLine($"Error: XPS file not found at '{xpsFile}'.");
            return;
        }

        try
        {
            // Initialize load options for XPS conversion.
            XpsLoadOptions loadOptions = new XpsLoadOptions();

            // Load the XPS document using the specified options.
            using (Document pdfDocument = new Document(xpsFile, loadOptions))
            {
                // Save the document as a regular PDF.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"Conversion successful. PDF saved to '{pdfFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}