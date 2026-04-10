using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where decrypted PDFs will be saved
        const string outputFolder = "DecryptedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"[ERROR] Input folder '{inputFolder}' does not exist. Nothing to process.");
            return;
        }

        // Lookup table: PDF file name (case‑insensitive) -> password
        var passwordLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Add entries as needed, e.g.:
            // { "report1.pdf", "Secret123" },
            // { "invoice.pdf", "Passw0rd!" }
        };

        // Process each PDF file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);

            // Retrieve the password for the current file
            if (!passwordLookup.TryGetValue(fileName, out string? password) || string.IsNullOrEmpty(password))
            {
                Console.Error.WriteLine($"[SKIP] No password found for '{fileName}'.");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the encrypted document using the supplied password
                using (Document doc = new Document(filePath, password))
                {
                    // Decrypt the document (no parameters)
                    doc.Decrypt();

                    // Save the decrypted version (overwrites if same name)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"[OK] Decrypted '{fileName}'.");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"[ERROR] Invalid password for '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ERROR] Failed to process '{fileName}': {ex.Message}");
            }
        }
    }
}
