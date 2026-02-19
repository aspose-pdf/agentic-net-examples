using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Directory that contains the source MHT file.
        // Adjust this path as needed for your environment.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input MHT file path.
        string mhtPath = Path.Combine(dataDir, "sample.mht");

        // Output PDF file path.
        string pdfPath = Path.Combine(dataDir, "sample.pdf");

        // Verify that the MHT file exists before attempting conversion.
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtPath}'.");
            return;
        }

        try
        {
            // Initialize load options for MHT files.
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the MHT document using the specified options.
            using (Document pdfDocument = new Document(mhtPath, loadOptions))
            {
                // Save the document as a regular PDF (non‑PDF/A).
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