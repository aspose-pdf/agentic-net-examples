using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class BatchDecrypt
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = @"C:\EncryptedPdfs";
        // Folder where decrypted PDFs will be saved
        const string outputFolder = @"C:\DecryptedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Lookup table of possible passwords (order matters)
        List<string> passwordLookup = new List<string>
        {
            "password1",
            "secret123",
            "letmein",
            "admin",
            // add more passwords as needed
        };

        // Process each PDF file in the input folder
        foreach (string encryptedPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(encryptedPath);
            string decryptedPath = Path.Combine(outputFolder, fileName);

            bool decrypted = false;

            // Try each password until one succeeds
            foreach (string pwd in passwordLookup)
            {
                try
                {
                    // Open the encrypted PDF with the current password
                    using (Document doc = new Document(encryptedPath, pwd))
                    {
                        // Remove encryption from the document
                        doc.Decrypt();

                        // Save the decrypted PDF
                        doc.Save(decryptedPath);
                    }

                    Console.WriteLine($"Decrypted: {fileName} using password \"{pwd}\"");
                    decrypted = true;
                    break; // stop trying passwords for this file
                }
                catch (InvalidPasswordException)
                {
                    // Password was incorrect – try the next one
                }
                catch (Exception ex)
                {
                    // Unexpected error – report and move to next file
                    Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
                    break;
                }
            }

            if (!decrypted)
            {
                Console.Error.WriteLine($"Failed to decrypt {fileName}: no matching password found.");
            }
        }
    }
}