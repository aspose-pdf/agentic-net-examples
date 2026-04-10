using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to protect
        const string folderPath = @"C:\PdfFolder";
        // Desired passwords
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Permissions to allow (example: printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

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
                // Open the PDF document
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
                Console.Error.WriteLine($"Invalid password error for '{pdfFile}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}