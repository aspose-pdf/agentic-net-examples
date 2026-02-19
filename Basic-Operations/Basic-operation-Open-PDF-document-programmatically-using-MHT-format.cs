using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source MHT file and where the PDF will be saved.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Full paths for input MHT and output PDF.
        string mhtFile = Path.Combine(dataDir, "MHT-to-PDF.mht");
        string pdfFile = Path.Combine(dataDir, "MHT-to-PDF.pdf");

        // Verify that the MHT file exists before attempting to load it.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        try
        {
            // Create load options for MHT conversion.
            MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

            // Load the MHT file into a PDF document using the specified options.
            using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
            {
                // Save the PDF document to the desired location.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"MHT successfully converted to PDF: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}