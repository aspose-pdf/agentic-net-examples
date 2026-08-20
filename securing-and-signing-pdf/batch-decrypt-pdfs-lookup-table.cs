using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // Decrypt() and Document constructors are here
using Aspose.Pdf;               // InvalidPasswordException is also here

class BatchDecrypt
{
    static void Main()
    {
        // Input folder containing encrypted PDFs
        const string inputFolder = @"C:\EncryptedPdfs";
        // Output folder for decrypted PDFs
        const string outputFolder = @"C:\DecryptedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Lookup table: PDF file name (without path) -> list of possible passwords
        var passwordLookup = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "document1.pdf", new List<string> { "password123", "secret" } },
            { "report2023.pdf", new List<string> { "ownerPass", "userPass" } },
            { "confidential.pdf", new List<string> { "admin", "12345", "letmein" } }
            // Add more entries as needed
        };

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.EnumerateFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            Console.WriteLine($"Processing: {fileName}");

            // Retrieve possible passwords; if none, skip
            if (!passwordLookup.TryGetValue(fileName, out List<string> possiblePasswords) ||
                possiblePasswords == null || possiblePasswords.Count == 0)
            {
                Console.WriteLine($"  No passwords defined for {fileName}. Skipping.");
                continue;
            }

            bool decrypted = false;

            // Try each password until the document opens successfully
            foreach (string pwd in possiblePasswords)
            {
                try
                {
                    // Open the encrypted PDF with the current password
                    using (Document doc = new Document(pdfPath, pwd))
                    {
                        // Decrypt the document (no parameters)
                        doc.Decrypt();

                        // Save decrypted PDF to the output folder (overwrite if exists)
                        string outputPath = Path.Combine(outputFolder, fileName);
                        doc.Save(outputPath);

                        Console.WriteLine($"  Decrypted with password \"{pwd}\" and saved to {outputPath}");
                        decrypted = true;
                    }

                    // Exit the password loop once decryption succeeds
                    break;
                }
                catch (InvalidPasswordException)
                {
                    // Password was incorrect; try the next one
                    Console.WriteLine($"  Password \"{pwd}\" failed.");
                }
                catch (Exception ex)
                {
                    // Unexpected error; report and move to next file
                    Console.WriteLine($"  Error processing {fileName}: {ex.Message}");
                    break;
                }
            }

            if (!decrypted)
            {
                Console.WriteLine($"  Failed to decrypt {fileName}: none of the supplied passwords were valid.");
            }
        }

        Console.WriteLine("Batch decryption completed.");
    }
}