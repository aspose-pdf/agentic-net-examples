using System;
using System.IO;
using Aspose.Pdf; // Document, MhtLoadOptions

class Program
{
    static void Main()
    {
        // Directory that contains the source MHT file.
        // Replace with the actual path where your MHT file is located.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Full path to the input MHT file.
        string mhtFile = Path.Combine(dataDir, "MHT-to-PDF.mht");

        // Full path for the output PDF file.
        string pdfFile = Path.Combine(dataDir, "MHT-to-PDF.pdf");

        // Verify that the source file exists before attempting to load it.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtFile}'.");
            return;
        }

        // Initialize load options for MHT conversion.
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Open the MHT file as a PDF document using the load options.
        // The Document is wrapped in a using block to ensure proper disposal.
        using (Document pdfDocument = new Document(mhtFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: '{pdfFile}'.");
    }
}