using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source MHT file and where the PDF will be saved.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Path to the input MHT file.
        string mhtFile = Path.Combine(dataDir, "input.mht");

        // Path to the output PDF file.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists before attempting to load it.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Initialize load options for MHT format.
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        // Load the MHT file into a PDF Document using the specified load options.
        // The Document is wrapped in a using block to ensure proper disposal.
        using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
        {
            // Save the resulting PDF document to the specified path.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: {pdfFile}");
    }
}