using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Included as requested (facade namespace)

class Program
{
    static void Main()
    {
        // Adjust this path to point to the folder that contains the MHT file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Full path to the source MHT file.
        string mhtPath = Path.Combine(dataDir, "MHT-to-PDF.mht");

        // Verify that the source file exists.
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Source file not found: {mhtPath}");
            return;
        }

        // Destination PDF file path.
        string pdfPath = Path.Combine(dataDir, "MHT-to-PDF.pdf");

        // Load the MHT file using the appropriate load options.
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Convert to PDF and save.
        using (Document pdfDocument = new Document(mhtPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"MHT file successfully converted to PDF:");
        Console.WriteLine($"  Source: {mhtPath}");
        Console.WriteLine($"  Output: {pdfPath}");
    }
}