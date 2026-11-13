using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        string inputFolder = "InputPdfs";
        // Folder where encrypted PDFs will be written
        string outputFolder = "ProtectedPdfs";

        // Define the passwords to apply to every PDF
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Set desired permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 (preferred algorithm)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF; extension remains .pdf
                    doc.Save(outPath);
                }

                Console.WriteLine($"Encrypted: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process {fileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}