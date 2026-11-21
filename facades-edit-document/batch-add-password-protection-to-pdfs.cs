using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to protect
        const string secureFolder = @"secure";
        // Password to apply to every PDF (user password)
        const string userPassword = "MySecurePassword";
        // Owner password (can be same as user password if desired)
        const string ownerPassword = "MySecurePassword";

        // Verify folder exists
        if (!Directory.Exists(secureFolder))
        {
            Console.Error.WriteLine($"Folder not found: {secureFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(secureFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Permissions to grant after encryption (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF
        foreach (string inputPath in pdfFiles)
        {
            // Output path – overwrite the original file
            string outputPath = inputPath;

            try
            {
                // Load the PDF with the Document API (PdfFileEditor does not support SetPassword)
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Apply password protection. AES‑128 is a common secure algorithm.
                    // Note: the overload expects (userPwd, ownerPwd, Permissions, CryptoAlgorithm)
                    pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Protected: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch password protection completed.");
    }
}
