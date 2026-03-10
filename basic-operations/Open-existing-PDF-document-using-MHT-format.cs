using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, MhtLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Path to the input MHT file.
        string dataDir = @"YOUR_DATA_DIRECTORY";
        string mhtFile = Path.Combine(dataDir, "input.mht");

        // Path to the output PDF file.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Ensure the input file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Initialize load options for MHT format.
        MhtLoadOptions mhtLoadOptions = new MhtLoadOptions();

        // Load the MHT file and convert it to a PDF document.
        using (Document pdfDocument = new Document(mhtFile, mhtLoadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: {pdfFile}");
    }
}