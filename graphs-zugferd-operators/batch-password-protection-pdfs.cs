using System;
using System.IO;
using Aspose.Pdf;

class BatchPasswordProtector
{
    static void Main()
    {
        // Folder containing PDF files
        const string folderPath = @"C:\PdfFolder";

        // Passwords to apply
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Permissions to grant for the user password
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Verify folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfFile))
                {
                    // Apply encryption with the specified passwords and permissions
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Overwrite the original file with the encrypted version
                    doc.Save(pdfFile);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfFile)}");
            }
            catch (InvalidPasswordException ex)
            {
                // This exception would occur if the file is already encrypted with a different password
                Console.Error.WriteLine($"Cannot encrypt (already protected) '{pdfFile}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch password protection completed.");
    }
}