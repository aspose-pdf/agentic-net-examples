using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the MHT file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input MHT file and output PDF file.
        string mhtFile = Path.Combine(dataDir, "input.mht");
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Initialize load options for MHT format.
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Load the MHT file and convert it to a PDF document.
        using (Document pdfDocument = new Document(mhtFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: {pdfFile}");
    }
}