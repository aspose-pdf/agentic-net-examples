using System;
using System.IO;
using Aspose.Pdf;               // Core namespace contains Document and OfdLoadOptions

class Program
{
    static void Main()
    {
        const string ofdPath   = "input.ofd";
        const string pdfPath   = "output.pdf";

        // Verify the source OFD file exists
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"Source file not found: {ofdPath}");
            return;
        }

        // Configure OFD loading options (default options are sufficient for a basic conversion)
        OfdLoadOptions loadOptions = new OfdLoadOptions();

        // Load the OFD document inside a using block for deterministic disposal
        using (Document doc = new Document(ofdPath, loadOptions))
        {
            // Save the loaded document as PDF; Document.Save without SaveOptions writes PDF by default
            doc.Save(pdfPath);
        }

        Console.WriteLine($"OFD successfully converted to PDF: {pdfPath}");
    }
}