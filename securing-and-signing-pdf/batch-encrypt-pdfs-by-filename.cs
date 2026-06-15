using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to encrypt
        const string inputFolder = @"C:\PdfFolder";
        // Folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfFolder\Encrypted";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the input folder (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Derive a password from the file name (without extension)
                string password = Path.GetFileNameWithoutExtension(pdfPath);

                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using the derived password for both user and owner,
                    // AES‑256 algorithm (preferred per encryption rule)
                    doc.Encrypt(userPassword: password,
                                ownerPassword: password,
                                permissions: perms,
                                cryptoAlgorithm: CryptoAlgorithm.AESx256);

                    // Build the output file path (same name, placed in the output folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                    // Save the encrypted PDF (PDF format, no SaveOptions needed)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)} → {outputFolder}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}