using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to protect
        const string inputFolder = @"C:\PdfFolder";
        // Optional: folder to place encrypted PDFs (can be same as inputFolder)
        const string outputFolder = @"C:\PdfFolder\Encrypted";

        // Passwords to apply to every PDF
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Permissions to grant (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Apply encryption with the specified passwords and permissions
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Determine output path (overwrite in place or write to output folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                    // Save the encrypted PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }
}