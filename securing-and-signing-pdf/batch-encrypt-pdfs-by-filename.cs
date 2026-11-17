using System;
using System.IO;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfFolder";
        // Output folder for encrypted PDFs (can be same as input)
        const string outputFolder = @"C:\PdfFolder\Encrypted";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the input folder (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Derive a password from the file name (without extension)
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string password = fileName; // simple derivation; adjust as needed

            // Build output path – keep original name with "_enc" suffix
            string encryptedPath = Path.Combine(outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + "_enc.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 (CryptoAlgorithm enum from Aspose.Pdf)
                    doc.Encrypt(userPassword: password,
                                ownerPassword: password,
                                permissions: perms,
                                cryptoAlgorithm: CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    doc.Save(encryptedPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)} → {encryptedPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{pdfPath}': {ex.Message}");
            }
        }
    }
}