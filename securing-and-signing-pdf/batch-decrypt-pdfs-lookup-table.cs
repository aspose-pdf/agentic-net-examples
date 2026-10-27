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

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Lookup table of possible passwords (order matters)
        List<string> passwordList = new List<string>
        {
            "password1",
            "secret2023",
            "letmein",
            "admin",
            "123456"
        };

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string encryptedPath in pdfFiles)
        {
            bool decrypted = false;
            string fileName = Path.GetFileNameWithoutExtension(encryptedPath);
            string decryptedPath = Path.Combine(outputFolder, fileName + "_decrypted.pdf");

            foreach (string pwd in passwordList)
            {
                try
                {
                    // Attempt to open the document with the current password
                    using (Document doc = new Document(encryptedPath, pwd))
                    {
                        // Decrypt the document (no parameters)
                        doc.Decrypt();

                        // Save the decrypted version
                        doc.Save(decryptedPath);
                    }

                    Console.WriteLine($"Decrypted '{Path.GetFileName(encryptedPath)}' using password '{pwd}'.");
                    decrypted = true;
                    break; // Exit password loop on success
                }
                catch (InvalidPasswordException)
                {
                    // Wrong password – try next one
                    continue;
                }
                catch (Exception ex)
                {
                    // Unexpected error – report and stop trying this file
                    Console.Error.WriteLine($"Error processing '{encryptedPath}': {ex.Message}");
                    break;
                }
            }

            if (!decrypted)
            {
                Console.Error.WriteLine($"Failed to decrypt '{Path.GetFileName(encryptedPath)}' – none of the passwords matched.");
            }
        }

        Console.WriteLine("Batch decryption completed.");
    }
}