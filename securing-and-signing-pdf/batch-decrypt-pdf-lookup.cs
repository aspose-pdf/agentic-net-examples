using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class PdfBatchDecryptor
{
    // Decrypts all PDF files in the input folder using the provided password lookup table.
    // Decrypted files are saved to the output folder with the same file name.
    public static void BatchDecrypt(string inputFolder, string outputFolder, Dictionary<string, string> passwordLookup)
    {
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enumerate PDF files (case‑insensitive) in the input folder.
        foreach (string inputPath in Directory.EnumerateFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Try to obtain the password for this file.
            if (!passwordLookup.TryGetValue(fileName, out string password))
            {
                Console.WriteLine($"No password found for '{fileName}'. Skipping.");
                continue;
            }

            try
            {
                // Open the encrypted PDF with the supplied password.
                using (Document doc = new Document(inputPath, password))
                {
                    // Decrypt the document (no parameters).
                    doc.Decrypt();

                    // Save the decrypted PDF. No SaveOptions are needed for PDF output.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Decrypted '{fileName}' → '{outputPath}'.");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"Invalid password for '{fileName}'. Skipping.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }

    // Example usage.
    static void Main()
    {
        // Folder containing encrypted PDFs.
        string inputFolder = @"C:\EncryptedPdfs";

        // Folder where decrypted PDFs will be written.
        string outputFolder = @"C:\DecryptedPdfs";

        // Lookup table: file name (including extension) → password.
        var passwordLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Report1.pdf", "pass123" },
            { "Invoice_A.pdf", "secret!" },
            { "Confidential.pdf", "admin2023" }
            // Add more entries as needed.
        };

        BatchDecrypt(inputFolder, outputFolder, passwordLookup);
    }
}