using System;
using System.IO;
using Aspose.Pdf;

class BatchPasswordProtector
{
    static void Main()
    {
        // Resolve the folders to absolute paths that work on any OS.
        string inputFolder = Path.GetFullPath(@"C:\PdfFolder");
        string outputFolder = Path.GetFullPath(@"C:\PdfFolder\Protected");

        // Verify that the input folder exists before trying to enumerate files.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output folder exists (creates it if necessary).
        Directory.CreateDirectory(outputFolder);

        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Example permissions – allow printing and content extraction.
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input folder.
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Load the PDF document.
                using (Document doc = new Document(inputPath))
                {
                    // Apply encryption with the specified passwords and permissions.
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF (overwrites if same path).
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (InvalidPasswordException ex)
            {
                // The source PDF is already password‑protected and cannot be opened.
                Console.Error.WriteLine($"Cannot open '{inputPath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch password protection completed.");
    }
}
