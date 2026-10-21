using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileInfo resides here

class Program
{
    static void Main()
    {
        // Resolve the base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input folder – create it if it does not exist so the sample can run out‑of‑the‑box
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder not found. Created empty folder at '{inputFolder}'. Place PDF files there and re‑run.");
        }

        // Output CSV file – also placed next to the executable
        string summaryFile = Path.Combine(baseDir, "EncryptionSummary.csv");

        // Gather all PDF files in the folder (if any)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Write header and results to CSV
        using (StreamWriter writer = new StreamWriter(summaryFile))
        {
            writer.WriteLine("File,Encrypted,Algorithm,Permissions");

            foreach (string pdfPath in pdfFiles)
            {
                try
                {
                    // PdfFileInfo provides encryption details without loading the full document
                    PdfFileInfo info = new PdfFileInfo(pdfPath);

                    bool isEncrypted = info.IsEncrypted;
                    // Aspose.Pdf currently does not expose algorithm or permission flags via PdfFileInfo.
                    // We report generic placeholders when encrypted, otherwise "None".
                    string algorithm   = isEncrypted ? "Encrypted" : "None";
                    string permissions = isEncrypted ? "Encrypted" : "None";

                    writer.WriteLine($"{Path.GetFileName(pdfPath)},{isEncrypted},{algorithm},{permissions}");
                }
                catch (Exception ex)
                {
                    // Log any errors but continue processing remaining files
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Encryption summary written to '{summaryFile}'.");
    }
}
