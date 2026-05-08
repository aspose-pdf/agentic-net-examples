using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the original PDFs
        const string inputFolder = "input_pdfs";
        // Folder where encrypted PDFs will be written
        const string outputFolder = "encrypted_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Derive passwords from the file name (without extension)
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string userPassword = baseName;               // user password
                string ownerPassword = baseName + "_owner";   // owner password

                // Define desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Encrypt using AES‑256 (preferred algorithm)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Construct the output file name
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"{baseName}_encrypted.pdf");

                    // Save the encrypted PDF (extension .pdf ensures PDF output)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {pdfPath} → {Path.Combine(outputFolder, $"{baseName}_encrypted.pdf")}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error encrypting '{pdfPath}': {ex.Message}");
            }
        }
    }
}