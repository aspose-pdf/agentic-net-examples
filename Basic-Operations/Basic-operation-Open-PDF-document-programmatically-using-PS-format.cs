using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input PostScript file and output PDF file paths
        string dataDir = @"YOUR_DATA_DIRECTORY";
        string psPath = Path.Combine(dataDir, "sample.ps");
        string pdfPath = Path.Combine(dataDir, "sample.pdf");

        // Verify that the PS file exists
        if (!File.Exists(psPath))
        {
            Console.Error.WriteLine($"Error: PostScript file not found at '{psPath}'.");
            return;
        }

        try
        {
            // Initialize load options for PS format
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Load the PS file into a Document object
            using (Document pdfDocument = new Document(psPath, loadOptions))
            {
                // Save the document as PDF
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Successfully converted PS to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}