using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // Not required here but kept for completeness

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to protect
        const string inputFolder = @"C:\PdfFolder";

        // Desired passwords
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Permissions to grant (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Verify the folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Apply password protection (encryption rule: use CryptoAlgorithm.AESx256)
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the protected document, overwriting the original file
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Protected: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}