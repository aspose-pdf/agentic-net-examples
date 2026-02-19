using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the files.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input MHT file.
        string mhtFile = Path.Combine(dataDir, "MHT-to-PDF.mht");

        // Output PDF file.
        string pdfFile = Path.Combine(dataDir, "MHT-to-PDF.pdf");

        // Verify the MHT file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Load options for MHT conversion.
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        // Load the MHT file into a PDF document.
        using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT successfully converted to PDF: {pdfFile}");
    }
}