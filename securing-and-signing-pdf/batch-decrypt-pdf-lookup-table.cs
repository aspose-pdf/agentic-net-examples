using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class BatchPdfDecryptor
{
    // Lookup table: PDF file name (without path) -> password
    private static readonly Dictionary<string, string> PasswordLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "protected1.pdf", "Password123" },
        { "confidential.pdf", "Secret!@#" },
        { "report.pdf", "Report2023" }
        // Add more entries as needed
    };

    static void Main()
    {
        // Folder containing the encrypted PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where decrypted PDFs will be saved
        const string outputFolder = @"C:\DecryptedPdfs";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.EnumerateFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            if (!PasswordLookup.TryGetValue(fileName, out string password))
            {
                Console.WriteLine($"[SKIP] No password found for '{fileName}'.");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the encrypted document using the password
                using (Document doc = new Document(inputPath, password))
                {
                    // Decrypt the document (no parameters)
                    doc.Decrypt();

                    // Save the decrypted version (overwrites if same name in output folder)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"[OK] Decrypted '{fileName}' -> '{outputPath}'.");
            }
            catch (InvalidPasswordException)
            {
                Console.WriteLine($"[ERROR] Invalid password for '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to process '{fileName}': {ex.Message}");
            }
        }
    }
}