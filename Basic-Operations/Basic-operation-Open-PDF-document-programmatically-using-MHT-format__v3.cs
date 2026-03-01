using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source MHT file and where the PDF will be saved.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input MHT file path.
        string mhtFile = Path.Combine(dataDir, "sample.mht");

        // Output PDF file path.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists before proceeding.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Initialize load options specific to MHT files.
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        // Open the MHT file as a PDF document using the load options.
        // The Document is wrapped in a using block for deterministic disposal.
        using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
        {
            // Save the resulting PDF document.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT successfully converted to PDF: {pdfFile}");
    }
}