using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Use the current working directory as the data folder.
        string dataDir = Directory.GetCurrentDirectory();

        // Input and output file names (generic, not hard‑coded to a specific sample).
        string mhtFile = Path.Combine(dataDir, "input.mht");
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the MHT file exists before proceeding.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"Error: MHT file not found at '{mhtFile}'.");
            return;
        }

        try
        {
            // MhtLoadOptions is a concrete class in the Aspose.Pdf namespace (not a nested type).
            var mhtLoadOptions = new Aspose.Pdf.MhtLoadOptions();

            // Document constructor that accepts a Stream and LoadOptions.
            using (var fileStream = File.OpenRead(mhtFile))
            using (var pdfDocument = new Document(fileStream, mhtLoadOptions))
            {
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"MHT successfully converted to PDF. Output saved at: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
