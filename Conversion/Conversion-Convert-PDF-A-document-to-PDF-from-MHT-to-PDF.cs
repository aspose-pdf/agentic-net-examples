using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source MHT file.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        // Path to the input MHT file.
        string mhtFile = Path.Combine(dataDir, "input.mht");

        // Desired path for the output PDF file.
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Verify that the source MHT file exists.
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }

        // Ensure the output directory exists.
        string outDir = Path.GetDirectoryName(pdfFile);
        if (!Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        try
        {
            // Load options specific for MHT files.
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the MHT file into a PDF document.
            using (Document pdfDocument = new Document(mhtFile, loadOptions))
            {
                // Save the document as a PDF file.
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"Conversion successful. PDF saved to: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}