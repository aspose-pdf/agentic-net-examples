using System;
using System.IO;
using Aspose.Pdf; // PsLoadOptions resides directly in this namespace

class Program
{
    static void Main(string[] args)
    {
        // Paths – adjust as needed
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string psFile = Path.Combine(dataDir, "sample.ps");      // Input PostScript file
        string pdfFile = Path.Combine(dataDir, "sample_converted.pdf"); // Output PDF file

        // Verify input file exists
        if (!File.Exists(psFile))
        {
            Console.Error.WriteLine($"Input file not found: {psFile}");
            return;
        }

        try
        {
            // Load the PS file with specific options
            PsLoadOptions psLoadOptions = new PsLoadOptions
            {
                // Example option: convert non‑TrueType fonts to TTF to reduce size
                ConvertFontsToTTF = true,
                // Disable font license checks if required
                DisableFontLicenseVerifications = false
            };

            // Load the document using the PS load options
            using (Document doc = new Document(psFile, psLoadOptions))
            {
                // Save the document as a regular PDF
                doc.Save(pdfFile);
            }

            Console.WriteLine($"Conversion successful. PDF saved to: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
